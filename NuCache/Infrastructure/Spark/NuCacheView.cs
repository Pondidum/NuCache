using System;
using NuCache.Models;
using Spark;

namespace NuCache.Infrastructure.Spark
{
	public abstract class NuCacheView : SparkViewBase
	{
	}

	public class NuCacheView<TViewModel> : NuCacheView, ISettableModel where TViewModel : class
	{
		public TViewModel Model { get; set; }
		public ApplicationViewModel SharedModel { get; private set; }

		public void SetModel(object model)
		{
			Model = (TViewModel)model;
		}

		public void SetSharedModel(ApplicationViewModel model)
		{
			SharedModel = model;
		}

		public override void Render()
		{
			RenderView(Output);
		}

		public override Guid GeneratedViewId
		{
			get { return Guid.NewGuid(); }
		}

		public override bool TryGetViewData(string name, out object value)
		{
			return base.TryGetViewData(name, out value);
		}
	}
}
