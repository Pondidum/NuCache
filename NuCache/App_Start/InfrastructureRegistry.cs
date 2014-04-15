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
			For<IPackageSource>().Use<ProxyingPackageSource>();
			For<IPackageCache>().Use<FileSystemPackageCache>().OnCreation(c => c.Initialise()).Singleton();
		}
	}
}
