using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NuCache.Infrastructure
{
	public class LoggingMessageHandler : DelegatingHandler
	{
		private readonly IRequestLogger _logger;

		public LoggingMessageHandler(IRequestLogger logger)
		{
			_logger = logger;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var sw = new Stopwatch();
			sw.Start();

			var response = await base.SendAsync(request, cancellationToken);

			sw.Stop();

			_logger.Log(request, response, sw.Elapsed);

			return response;
		}
	}
}
