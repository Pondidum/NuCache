using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Infrastructure.Statistics;
using NuCache.Infrastructure.Statistics.Processors;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class StatisticsController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;
		private readonly StatisticsCollector _statistics;

		public StatisticsController(SparkResponseFactory responseFactory, StatisticsCollector statistics)
		{
			_responseFactory = responseFactory;
			_statistics = statistics;
		}

		public HttpResponseMessage GetIndex()
		{
			var downloads = _statistics.GetStatistic<PackageDownloads>();

			var model = new StatisticsViewModel
			{
				DownloadCounts = downloads.PackageCounts
			};

			return _responseFactory.From(model);
		}
	}
}
