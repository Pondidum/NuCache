using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Should;
using Xunit;

namespace NuCache.Tests.RewriterTests
{
	public class EntryXmlRewriterTests : XmlRewriterTestBase
	{
		private readonly XDocument _expected;

		public EntryXmlRewriterTests()
			: base("NuCache.Tests.Entry.xml")
		{
			using (var inputStream = GetType().Assembly.GetManifestResourceStream("NuCache.Tests.Entry.Expected.xml"))
			{
				_expected = XDocument.Load(inputStream);
			}
		}

		[Fact]
		public void When_processing_query_entry_xml()
		{
			var urls = _result
				.Descendants(_namespace + "entry")
				.Elements(_namespace + "content")
				.Attributes("src")
				.Select(a => new Uri(a.Value))
				.ToList();
			
			foreach (var url in urls)
			{
				ValidateUri(url);
			}
		}
	}
}
