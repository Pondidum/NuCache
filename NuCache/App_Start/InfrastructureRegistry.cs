using NuCache.Infrastructure;
using StructureMap.Configuration.DSL;

namespace NuCache.App_Start
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			For<WebClient>().Use<WebClient>();
			For<PackageCache>().Singleton().Use<PackageCache>();
		}
	}
}
