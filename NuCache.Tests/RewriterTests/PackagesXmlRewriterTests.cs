using System;
using System.Linq;
using System.Xml.Linq;
using Should;
using Xunit;

namespace NuCache.Tests.RewriterTests
{
	public class PackagesXmlRewriterTests : XmlRewriterTestBase
	{
		public PackagesXmlRewriterTests()
			: base("NuCache.Tests.Packages.xml")
		{
		}

		[Fact]
		public void When_rewriting_all_entry_content_elements_should_be_repointed()
		{
			var urls = _result.Root
				.Elements(_namespace + "entry")
				.Elements(_namespace + "content")
				.Attributes("src")
				.Select(a => new Uri(a.Value))
				.ToList();

			urls.Count.ShouldEqual(2);

			foreach (var url in urls)
			{
				ValidateUri(url);
			}
		}

		[Fact]
		public void When_rewriting_any_link_elements_should_be_repointed()
		{
			var urls = _result.Root
				.Elements(_namespace + "link")
				.Where(e => e.Attribute("rel").Value == "next")
				.Attributes("href")
				.Select(a => new Uri(a.Value))
				.ToList();

			foreach (var url in urls)
			{
				ValidateUri(url);
			}
		}

		[Fact]
		public void When_rewriting_all_entry_content_ids_should_be_repointed()
		{
			var urls = _result.Root
				.Elements(_namespace + "entry")
				.Elements(_namespace + "id")
				.Select(e => new Uri(e.Value))
				.ToList();

			urls.Count.ShouldEqual(2);

			foreach (var url in urls)
			{
				ValidateUri(url);
			}

		}

		[Fact]
		public void When_rewriting_the_feed_base_url_should_be_repointed()
		{
			var url = _result.Root
				.Attributes()
				.Where(a => a.Name.LocalName == "base")
				.Select(a => new Uri(a.Value))
				.First();

			ValidateUri(url);
		}

		[Fact]
		public void When_rewriting_the_feed_id_url_should_be_repointed()
		{
			var urls = _result.Root
				.Elements("id")
				.Select(a => new Uri(a.Value));

			foreach (var url in urls)
			{
				ValidateUri(url);
			}
		}
	}
}
