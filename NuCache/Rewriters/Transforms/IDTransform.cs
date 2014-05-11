using System;
using System.Xml.Linq;

namespace NuCache.Rewriters.Transforms
{
	public class IDTransform :IXElementTransform
	{
		public void Transform(XElement node, Func<string, Uri> urlTransform)
		{
			if (node.Name.LocalName != "id")
			{
				return;
			}

			node.SetValue(urlTransform(node.Value));
		}
	}
}
