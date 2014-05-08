using Spark;

namespace NuCache.Infrastructure.Spark
{
	public interface ISparkEngine
	{
		SparkViewBase CreateView<TModel>(TModel model) where TModel : class;
	}
}
