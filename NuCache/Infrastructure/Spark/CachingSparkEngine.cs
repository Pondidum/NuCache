using System;
using System.Collections.Generic;
using Spark;

namespace NuCache.Infrastructure.Spark
{
	public class CachingSparkEngine : ISparkEngine
	{
		private readonly Dictionary<Type, SparkViewBase> _viewCache;
		private readonly ISparkEngine _engine;

		public CachingSparkEngine(ISparkEngine engine)
		{
			_engine = engine;
			_viewCache = new Dictionary<Type, SparkViewBase>();
		}

		public SparkViewBase CreateView<TModel>(TModel model) where TModel : class
		{
			var modelType = typeof(TModel);

			if (_viewCache.ContainsKey(modelType) == false)
			{
				_viewCache[modelType] = _engine.CreateView(model);
			}

			return _viewCache[modelType];
		}
	}
}
