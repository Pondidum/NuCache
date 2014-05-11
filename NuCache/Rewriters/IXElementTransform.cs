using System;
using System.Xml.Linq;

namespace NuCache.Rewriters
{
	public interface IXElementTransform
	{
		void Transform(XElement node, Func<string, Uri> urlTransform);
	}
}
