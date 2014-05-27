﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace NuCache.Rewriters
{
	public class XmlRewriter
	{
		private readonly UriRewriter _uriRewriter;

		public XmlRewriter(UriRewriter uriRewriter)
		{
			_uriRewriter = uriRewriter;
		}

		public virtual void Rewrite(Uri targetUri, Stream inputStream, Stream outputStream)
		{
			Func<String, Uri> transform = url => _uriRewriter.TransformHost(targetUri, new Uri(url));

			var doc = XDocument.Load(inputStream);

			var root = doc.Root;
			var ns = root.Name.Namespace;

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

			var entries = doc
				.Descendants(ns + "entry")
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

		//private void ProcessFeedAttributes(XmlReader reader, XmlWriter writer, Func<string, Uri> transform)
		//{
		//	var xEle = new XElement(reader.LocalName);
		//	var map = new Dictionary<XAttribute, string>();

		//	do
		//	{
		//		var xAttribute =
		//			new XAttribute(
		//				XNamespace.Get((reader.Prefix.Length == 0) ? string.Empty : reader.NamespaceURI).GetName(reader.LocalName),
		//				reader.Value);
		//		xEle.Add(xAttribute);
		//		map.Add(xAttribute, reader.Prefix);
		//	} while (reader.MoveToNextAttribute());

		//	ProcessReplacement(xEle, transform);

		//	foreach (var pair in map)
		//	{
		//		var xAttribute = pair.Key;
		//		var prefix = pair.Value;

		//		writer.WriteStartAttribute(prefix, xAttribute.Name.LocalName, xAttribute.Name.NamespaceName);
		//		writer.WriteString(xAttribute.Value);
		//		writer.WriteEndAttribute();
		//	}
		//}
	}
}
