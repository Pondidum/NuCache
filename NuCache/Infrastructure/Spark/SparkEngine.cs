using System;
using System.IO;
using NuCache.Models;
using Spark;

namespace NuCache.Infrastructure.Spark
{
	public class SparkEngine : ISparkEngine
	{
		private readonly ApplicationViewModel _sharedModel;
		private readonly SparkViewEngine _engine;

		public SparkEngine(ApplicationViewModel sharedModel)
		{
			_sharedModel = sharedModel;
			var settings = new SparkSettings();
			settings.AddNamespace("System.Linq");
			settings.PageBaseType = typeof(NuCacheView).FullName;

			_engine = new SparkViewEngine(settings);
		}

		public ISettableModel CreateView<TModel>(TModel model) where TModel : class
		{
			var view = GetView(typeof(TModel));
			view.SetModel(model);
			view.SetSharedModel(_sharedModel);

			return view;
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