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
			var url = new Uri("http://nuget.org");

			For<UriHostTransformer>().Use<UriHostTransformer>().Ctor<Uri>().Is(url);
			For<IPackageSource>().Use<RemotePackageSource>();

			For<WebClient>().Use<WebClient>();
			For<PackageCache>().Singleton().Use<PackageCache>();
		}
	}
}
