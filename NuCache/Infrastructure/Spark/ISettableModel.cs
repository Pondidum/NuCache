using System;
using System.IO;
using NuCache.Models;

namespace NuCache.Infrastructure.Spark
{
	public interface ISettableModel
	{
		void SetModel(Object model);
		void SetSharedModel(ApplicationViewModel model);
		void RenderView(TextWriter writer);
	}
}