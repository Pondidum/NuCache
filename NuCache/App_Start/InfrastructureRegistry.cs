using System;
using NuCache.Infrastructure;
using NuCache.PackageSources;
using StructureMap.Configuration.DSL;

namespace NuCache.App_Start
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			For<UriHostTransformer>().Use<UriHostTransformer>();
			For<IPackageSource>().Use<RemotePackageSource>();
			For<ApplicationSettings>().Singleton().Use<ApplicationSettings>();
			For<WebClient>().Use<WebClient>();
			For<PackageCache>().Singleton().Use<PackageCache>();
		}
	}
}
