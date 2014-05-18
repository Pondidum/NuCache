using NuCache.Infrastructure.Statistics;
using NuCache.PackageSources;
using NuCache.ProxyBehaviour;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace NuCache.App_Start
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			For<StatisticsCollector>()
				.Use<StatisticsCollector>()
				.Singleton();

			For<IPackageSource>()
				.Use<ProxyingPackageSource>()
				.DecorateWith((container, original) => new StatisticsPackageSource(original, container.GetInstance<StatisticsCollector>()));

			For<IPackageCache>()
				.Use<FileSystemPackageCache>()
				.OnCreation(c => c.Initialise())
				.Singleton();

		}
	}
}
