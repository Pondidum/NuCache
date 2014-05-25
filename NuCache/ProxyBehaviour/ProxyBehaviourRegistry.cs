using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace NuCache.ProxyBehaviour
{
	public class ProxyBehaviourRegistry : Registry
	{
		public ProxyBehaviourRegistry ()
		{
			Scan(a =>
			{
				a.TheCallingAssembly();
				a.AddAllTypesOf<IProxyBehaviour>();
			});
		}
	}
}
