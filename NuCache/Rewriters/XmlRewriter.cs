using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NuCache.Rewriters
{
	public class XmlRewriter
	{
		private readonly UriHostTransformer _uriRewriter;

		public XmlRewriter(UriHostTransformer uriRewriter)
		{
			_uriRewriter = uriRewriter;
		}

		public void Rewrite(Uri targetUri, Stream inputStream, Stream outputStream)
		{
			var doc = XDocument.Load(inputStream);
			var ns = doc.Root.Name.Namespace;

			var attributes = doc.Root
				.Elements(ns + "entry")
				.SelectMany(e => e.Elements(ns + "content"))
				.Select(e => e.Attribute("src"));

			foreach (var attribute in attributes)
			{
				var url = new Uri(attribute.Value);
				attribute.SetValue(_uriRewriter.Transform(targetUri, url));
			}

			doc.Save(outputStream);
		}
	}
}
