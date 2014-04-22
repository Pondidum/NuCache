using System.Net;
using System.Net.Http;
using System.Web.Http;
using NuCache.Infrastructure.Spark;
using NuCache.Models;

namespace NuCache.Controllers
{
	public class ErrorController : ApiController
	{
		private readonly SparkResponseFactory _responseFactory;

		public ErrorController(SparkResponseFactory responseFactory)
		{
			_responseFactory = responseFactory;
		}

		[HttpGet][HttpPost][HttpPut][HttpPatch][HttpDelete]
		public HttpResponseMessage Handle404()
		{
			var model = new ErrorViewModel();
			var response = _responseFactory.From(model);
			response.StatusCode = HttpStatusCode.NotFound;
			response.ReasonPhrase = "The requested resource is not found";

			return response;
		}
	}
}