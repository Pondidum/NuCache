using System;
using NuCache.Controllers;
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

			For<IPackageSource>().Use<RemotePackageSource>().Ctor<Uri>().Is(url);

			For<WebClient>().Use<WebClient>();
			For<PackageCache>().Singleton().Use<PackageCache>();
		}
	}
}
