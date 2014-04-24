using System.Net;
using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class ManageController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;

		public ManageController(SparkResponseFactory responseFactory)
		{
			_responseFactory = responseFactory;
		}

		public HttpResponseMessage GetIndex()
		{
			return _responseFactory.From(new ManageViewModel());
		}
	}
}
