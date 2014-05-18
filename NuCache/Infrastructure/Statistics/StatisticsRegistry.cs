using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace NuCache.Infrastructure.Statistics
{
	public class StatisticsRegistry : Registry
	{
		public StatisticsRegistry()
		{
			Scan(s =>
			{
				s.TheCallingAssembly();
				s.AddAllTypesOf<IStatistic<HttpStatistic>>();
			});

			
			
		}
	}
}
