using System;
using System.Xml.Linq;

namespace NuCache.Rewriters.Transforms
{
	public class LinkNextTransform : IXElementTransform
	{
		public void Transform(XElement node, Func<string, Uri> urlTransform)
		{
			if (node.Name.LocalName != "link")
			{
				return;
			}

			if (node.Attribute("rel").Value != "next")
			{
				return;
			}

			var attribute = node.Attribute("href");
			attribute.SetValue(urlTransform(attribute.Value));
		}
	}
}
