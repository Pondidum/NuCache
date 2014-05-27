using System.IO;
using System.Xml.Linq;
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

		}
	}
}
