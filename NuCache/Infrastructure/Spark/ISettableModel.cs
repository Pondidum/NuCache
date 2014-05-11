using System;
using System.IO;

namespace NuCache.Infrastructure.Spark
{
	public interface ISettableModel
	{
		void SetModel(Object model);
		void RenderView(TextWriter writer);
	}
}