using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NuCache.Rewriters;
using Should;

namespace NuCache.Tests.RewriterTests
{
	public class XmlRewriterTests
	{
		private readonly XDocument _result;
		private readonly XNamespace _namespace;

		public XmlRewriterTests()
		{
			var asm = GetType().Assembly;


			var uriRewriter = new UriHostTransformer();
			var rewriter = new XmlRewriter(uriRewriter);
			var targetUri = new Uri("http://localhost:42174/");

			using (var inputStream = asm.GetManifestResourceStream("NuCache.Tests.Packages.xml"))
			using (var outputStream = new MemoryStream())
			{
				rewriter.Rewrite(targetUri, inputStream, outputStream);
				outputStream.Position = 0;

				_result = XDocument.Load(outputStream);
				_namespace = _result.Root.Name.Namespace;
			}

		}

		private void ValidateUri(Uri url)
		{
			url.Host.ShouldEqual("localhost");
			url.Port.ShouldEqual(42174);
		}

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
	}
}
