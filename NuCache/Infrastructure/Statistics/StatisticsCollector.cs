using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using NuCache.Infrastructure.NuGet;

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

		public void Log(HttpRequestMessage request, HttpResponseMessage response, PackageID packageID)
		{
			var container = new HttpStatistic(request.RequestUri, response.StatusCode, packageID.Name, packageID.Version);

			_store.Append(container);
			_statisticProcessors.ForEach(sp => sp.Process(container));
		}

		public T GetStatistic<T>() where T : IStatistic<HttpStatistic>
		{
			return _statisticProcessors.OfType<T>().FirstOrDefault();
		}
	}
}
