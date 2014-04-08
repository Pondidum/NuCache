using System;
using NuCache.Infrastructure;
using NuCache.PackageSources;
using NuCache.Rewriters;
using StructureMap.Configuration.DSL;

namespace NuCache.App_Start
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			For<UriRewriter>().Use<UriRewriter>();
			For<IPackageSource>().Use<ProxyingPackageSource>();
			For<ApplicationSettings>().Singleton().Use<ApplicationSettings>();
			For<WebClient>().Use<WebClient>();
		}
	}
}
