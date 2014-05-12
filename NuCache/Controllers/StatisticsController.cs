using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class StatisticsController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;

		public StatisticsController(SparkResponseFactory responseFactory)
		{
			_responseFactory = responseFactory;
		}

		public HttpResponseMessage GetIndex()
		{
			return _responseFactory.From(new StatisticsViewModel());
		}
	}
}
