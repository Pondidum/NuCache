using System;
using System.IO;
using System.Xml.Linq;
using NuCache.Rewriters;
using Should;

namespace NuCache.Tests.RewriterTests
{
	public class XmlRewriterTestBase
	{
		protected readonly XDocument _result;
		protected readonly XNamespace _namespace;

		public XmlRewriterTestBase(string resourceName)
		{

			var rewriter = new XmlRewriter(new UriRewriter());
			var targetUri = new Uri("http://localhost:42174/");

			using (var inputStream = GetType().Assembly.GetManifestResourceStream(resourceName))
			using (var outputStream = new MemoryStream())
			{
				rewriter.Rewrite(targetUri, inputStream, outputStream);
				outputStream.Position = 0;

				_result = XDocument.Load(outputStream);
				_namespace = _result.Root.Name.Namespace;
			}

		}

		protected void ValidateUri(Uri url)
		{
			url.Host.ShouldEqual("localhost");
			url.Port.ShouldEqual(42174);
		}

	}
}
