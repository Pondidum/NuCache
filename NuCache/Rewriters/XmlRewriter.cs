using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

		private void ProcessReplacement(XElement node, Func<string, Uri> transform)
		{
			if (node.Name.LocalName == "link" && node.Attribute("rel").Value == "next")
			{
				var attribute = node.Attribute("href");
				attribute.SetValue(transform(attribute.Value));
			}

			node
				.Attributes()
				.Where(a => a.Name.LocalName == "base")
				.ForEach(a => a.SetValue(transform(a.Value)));

			if (node.Name.LocalName == "entry")
			{
				node
					.Elements()
					.Where(e => e.Name.LocalName == "content")
					.Attributes("src")
					.ForEach(a => a.SetValue(transform(a.Value)));

				node
					.Elements()
					.Where(e => e.Name.LocalName == "id")
					.ForEach(e => e.SetValue(transform(e.Value)));
			}

			if (node.Name.LocalName == "id")
			{
				node.SetValue(transform(node.Value));
			}
		}

		public virtual void Rewrite(Uri targetUri, Stream inputStream, Stream outputStream)
		{
			Func<String, Uri> transform = url => _uriRewriter.TransformHost(targetUri, new Uri(url));

			using (var reader = XmlReader.Create(inputStream))
			using (var writer = XmlWriter.Create(outputStream))
			{

				writer.WriteStartDocument();
				reader.MoveToContent();

				writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);

				ProcessFeedAttributes(reader, writer, transform);

				while (reader.Read())
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						var element = (XElement)XNode.ReadFrom(reader);
						ProcessReplacement(element, transform);

						element.WriteTo(writer);
					}
				}

				writer.WriteEndElement();
			}
		}

		private void ProcessFeedAttributes(XmlReader reader, XmlWriter writer, Func<string, Uri> transform)
		{
			var xEle = new XElement(reader.LocalName);
			var map = new Dictionary<XAttribute, string>();

			do
			{
				var xAttribute =
					new XAttribute(
						XNamespace.Get((reader.Prefix.Length == 0) ? string.Empty : reader.NamespaceURI).GetName(reader.LocalName),
						reader.Value);
				xEle.Add(xAttribute);
				map.Add(xAttribute, reader.Prefix);
			} while (reader.MoveToNextAttribute());

			ProcessReplacement(xEle, transform);

			foreach (var pair in map)
			{
				var xAttribute = pair.Key;
				var prefix = pair.Value;

				writer.WriteStartAttribute(prefix, xAttribute.Name.LocalName, xAttribute.Name.NamespaceName);
				writer.WriteString(xAttribute.Value);
				writer.WriteEndAttribute();
			}
		}
	}
}
