using System;
using System.IO;
using Spark;

namespace NuCache.Infrastructure.Spark
{
	public class SparkEngine : ISparkEngine
	{
		private readonly SparkViewEngine _engine;

		public SparkEngine()
		{
			var settings = new SparkSettings();
			settings.AddNamespace("System.Linq");
			settings.PageBaseType = typeof(NuCacheView).FullName;

			_engine = new SparkViewEngine(settings);
		}

		public SparkViewBase CreateView<TModel>(TModel model) where TModel : class
		{
			var view = GetView(typeof(TModel));
			view.SetModel(model);

			return (SparkViewBase)view;
		}

		private ISettableModel GetView(Type modelType)
		{
			var modelName = modelType.Name;
			var viewName = String.Format("{0}.spark", modelName.Replace("ViewModel", ""));

			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(viewName);
			descriptor.AddTemplate(Path.Combine("Shared", "Application.spark"));

			var entry = _engine.CreateEntry(descriptor);

			return (ISettableModel)entry.CreateInstance();
		}
	}
}