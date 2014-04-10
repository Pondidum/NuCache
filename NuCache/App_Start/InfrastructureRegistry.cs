using System;
using NuCache.Infrastructure;
using NuCache.PackageSources;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace NuCache.App_Start
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();
				a.AddAllTypesOf<IProxyBehaviour>();
			
				For<ProxyBehaviourSet>().Use(x => new ProxyBehaviourSet(x.GetAllInstances<IProxyBehaviour>()));
				For<IPackageSource>().Use<ProxyingPackageSource>();
				For<IPackageCache>().Use<FileSystemPackageCache>();
			});
		}
	}
}
