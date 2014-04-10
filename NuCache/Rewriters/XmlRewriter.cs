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

			var root = doc.Root;
			var ns = root.Name.Namespace;

			Func<String, Uri> transform = url => _uriRewriter.TransformHost(targetUri, new Uri(url));

			root
				.Elements(ns + "link")
				.Where(e => e.Attribute("rel").Value == "next")
				.Attributes("href")
				.ForEach(a => a.SetValue(transform(a.Value)));

			root
				.Attributes()
				.Where(a => a.Name.LocalName == "base")
				.ForEach(a => a.SetValue(transform(a.Value)));

			root
				.Elements("id")
				.ForEach(a => a.SetValue(transform(a.Value)));


			var entries = root
				.Elements(ns + "entry")
				.ToList();

			entries
				.Elements(ns + "content")
				.Attributes("src")
				.ForEach(a => a.SetValue(transform(a.Value)));

			entries
				.Elements(ns + "id")
				.ForEach(a => a.SetValue(transform(a.Value)));

			doc.Save(outputStream);
		}
	}
}
