using System;
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

		private void ProcessReplacement(XElement node, Func<string, Uri> transform)
		{
			if (node.Name.LocalName == "link" && node.Attribute("rel").Value == "next")
			{
				var attribute = node.Attribute("href");
				attribute.SetValue(transform(attribute.Value));
			}

			/* on FEED root.
			
			node
				.Attributes()
				.Where(a => a.Name.LocalName == "base")
				.ForEach(a => a.SetValue(transform(a.Value)));
			//root
			//	.Attributes()
			//	.Where(a => a.Name.LocalName == "base")
			//	.ForEach(a => a.SetValue(transform(a.Value)));
			
			*/

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
				writer.WriteAttributes(reader, false);

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

		

			//var doc = XDocument.Load(inputStream);

			//var root = doc.Root;
			//var ns = root.Name.Namespace;

			

			//root
			//	.Elements(ns + "link")
			//	.Where(e => e.Attribute("rel").Value == "next")
			//	.Attributes("href")
			//	.ForEach(a => a.SetValue(transform(a.Value)));

			//root
			//	.Attributes()
			//	.Where(a => a.Name.LocalName == "base")
			//	.ForEach(a => a.SetValue(transform(a.Value)));

			//root
			//	.Elements("id")
			//	.ForEach(a => a.SetValue(transform(a.Value)));


			//var entries = root
			//	.Elements(ns + "entry")
			//	.ToList();

			//entries
			//	.Elements(ns + "content")
			//	.Attributes("src")
			//	.ForEach(a => a.SetValue(transform(a.Value)));

			//entries
			//	.Elements(ns + "id")
			//	.ForEach(a => a.SetValue(transform(a.Value)));

			//doc.Save(outputStream);
		}
	}
}
