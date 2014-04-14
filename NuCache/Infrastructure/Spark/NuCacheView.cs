using System;
using Spark;

namespace NuCache.Infrastructure.Spark
{
	public abstract class NuCacheView : SparkViewBase
	{
	}

	public class NuCacheView<TViewModel> : NuCacheView, ISettableModel where TViewModel : class
	{
		public TViewModel Model { get; set; }

		public void SetModel(object model)
		{
			Model = (TViewModel)model;
		}

		public override void Render()
		{
			RenderView(Output);
		}

		public override Guid GeneratedViewId
		{
			get { return Guid.NewGuid(); }
		}
	}
}
