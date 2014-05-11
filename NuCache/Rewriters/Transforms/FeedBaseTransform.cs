using System;
using System.Linq;
using System.Xml.Linq;

namespace NuCache.Rewriters.Transforms
{
	public class FeedBaseTransform : IXElementTransform
	{
		public void Transform(XElement node, Func<string, Uri> urlTransform)
		{
			if (node.Name.LocalName != "feed")
			{
				return;
			}

			node
				.Attributes()
				.Where(a => a.Name.LocalName == "base")
				.ForEach(a => a.SetValue(urlTransform(a.Value)));
		}
	}
}
