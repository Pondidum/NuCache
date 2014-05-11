using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace NuCache.Rewriters
{
	public class RewriterRegistry : Registry
	{
		public RewriterRegistry()
		{
			Scan(s =>
			{
				s.TheCallingAssembly();
				s.WithDefaultConventions();

				s.AddAllTypesOf<IXElementTransform>();
			});
		}
	}
}
