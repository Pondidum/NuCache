using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace NuCache.Infrastructure.Statistics
{
	public class StatisticsCollector
	{
		private readonly SatisticsStore _store;
		private readonly IEnumerable<IStatistic<HttpStatistic>> _statisticProcessors;

		public StatisticsCollector(SatisticsStore store, IEnumerable<IStatistic<HttpStatistic>> statisticProcessors)
		{
			_store = store;
			_statisticProcessors = statisticProcessors;

			_store.BeginLoadFromDisk(container => _statisticProcessors.ForEach(sp => sp.Process(container)));
		}

		public void Log(HttpRequestMessage request, HttpResponseMessage response, string name, string version)
		{
			var container = new HttpStatistic(request.RequestUri, response.StatusCode, name, version);

			_store.Append(container);
			_statisticProcessors.ForEach(sp => sp.Process(container));
		}

		public T GetStatistic<T>() where T : IStatistic<HttpStatistic>
		{
			return _statisticProcessors.OfType<T>().FirstOrDefault();
		}
	}
}
