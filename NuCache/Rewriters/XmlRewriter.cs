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
		private readonly IEnumerable<IXElementTransform> _transforms;
		private readonly UriRewriter _uriRewriter;

		public XmlRewriter(IEnumerable<IXElementTransform> transforms, UriRewriter uriRewriter)
		{
			_transforms = transforms;
			_uriRewriter = uriRewriter;
		}

		private void ProcessReplacement(XElement node, Func<string, Uri> transform)
		{
			_transforms.ForEach(t => t.Transform(node, transform));
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
