using StructureMap.Configuration.DSL;

namespace NuCache.Infrastructure
{
	public class InfrastructureRegistry : Registry
	{
		public InfrastructureRegistry()
		{
			For<IPackageCache>()
				.Use<FileSystemPackageCache>()
				.OnCreation(c => c.Initialise())
				.Singleton();
		}
	}
}
