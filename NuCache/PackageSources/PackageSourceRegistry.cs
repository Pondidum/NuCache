using NuCache.Infrastructure.Statistics;
using StructureMap.Configuration.DSL;

namespace NuCache.PackageSources
{
	public class PackageSourceRegistry : Registry
	{
		public PackageSourceRegistry()
		{
			For<IPackageSource>()
				.Use<ProxyingPackageSource>()
				.DecorateWith((container, original) => new StatisticsPackageSource(original, container.GetInstance<StatisticsCollector>()));

		}
	}
}
