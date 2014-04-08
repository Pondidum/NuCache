using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NuCache.Infrastructure;

namespace NuCache.Rewriters
{
	public class XmlRewriter
	{
		private readonly UriRewriter _uriRewriter;

		public XmlRewriter(UriRewriter uriRewriter)
		{
			_uriRewriter = uriRewriter;
		}

		public void Rewrite(Uri targetUri, Stream inputStream, Stream outputStream)
		{
			var doc = XDocument.Load(inputStream);
			var ns = doc.Root.Name.Namespace;

			Func<String, Uri> transform = url => _uriRewriter.TransformHost(targetUri, new Uri(url));

			var elements = doc.Root
				.Elements(ns + "entry")
				.ToList();

			elements
				.Elements(ns + "content")
				.Attributes("src")
				.ForEach(a => a.SetValue(transform(a.Value)));

			elements
				.Elements(ns + "id")
				.ForEach(a => a.SetValue(transform(a.Value)));

			doc.Root
				.Elements(ns + "link")
				.Where(e => e.Attribute("rel").Value == "next")
				.Attributes("href")
				.ForEach(a => a.SetValue(transform(a.Value)));

			doc.Save(outputStream);
		}
	}
}
