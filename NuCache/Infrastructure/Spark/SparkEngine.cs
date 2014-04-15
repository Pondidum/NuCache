using System;
using System.Collections.Generic;
using System.IO;
using Spark;

namespace NuCache.Infrastructure.Spark
{
	public class SparkEngine
	{
		private readonly SparkViewEngine _engine;
		private readonly Dictionary<Type, ISettableModel> _viewCache;

		public SparkEngine()
		{
			_viewCache = new Dictionary<Type, ISettableModel>();

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
			if (_viewCache.ContainsKey(modelType))
			{
				return _viewCache[modelType];
			}

			var modelName = modelType.Name;
			var viewName = String.Format("{0}.spark", modelName.Replace("ViewModel", ""));

			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(viewName);
			descriptor.AddTemplate(Path.Combine("Shared", "Application.spark"));

			var entry = _engine.CreateEntry(descriptor);
			var instance = (ISettableModel)entry.CreateInstance();

			_viewCache[modelType] = instance;

			return instance;
		}
	}
}