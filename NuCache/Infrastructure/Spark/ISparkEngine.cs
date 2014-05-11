using Spark;

namespace NuCache.Infrastructure.Spark
{
	public interface ISparkEngine
	{
		ISettableModel CreateView<TModel>(TModel model) where TModel : class;
	}
}
