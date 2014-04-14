using StructureMap.Configuration.DSL;

namespace NuCache.Infrastructure.Spark
{
	public class SparkRegistry : Registry
	{
		public SparkRegistry()
		{
			For<SparkEngine>().Use<SparkEngine>().Singleton();
		}
	}
}
