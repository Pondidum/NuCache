using System;
using System.Linq;
using System.Xml.Linq;

namespace NuCache.Rewriters.Transforms
{
	public class EntryIDTransform : IXElementTransform
	{
		public void Transform(XElement node, Func<string, Uri> urlTransform)
		{
			if (node.Name.LocalName != "entry")
			{
				return;
			}

			node
				.Elements()
				.Where(e => e.Name.LocalName == "id")
				.ForEach(e => e.SetValue(urlTransform(e.Value)));
		}
	}
}
