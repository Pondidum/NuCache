using StructureMap.Configuration.DSL;

namespace NuCache.Infrastructure.Spark
{
	public class SparkRegistry : Registry
	{
		public SparkRegistry()
		{
#if DEBUG
			For<ISparkEngine>().Use<SparkEngine>().Singleton();
#else
			For<ISparkEngine>().Use(() => new CachingSparkEngine(new SparkEngine())).Singleton();
#endif
		}
	}
}
