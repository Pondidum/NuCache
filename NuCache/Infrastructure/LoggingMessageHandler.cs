using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NuCache.Infrastructure
{
	public class LoggingMessageHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var sw = new Stopwatch();
			sw.Start();

			var response = await base.SendAsync(request, cancellationToken);

			sw.Stop();

			Log(request, response, sw.Elapsed);

			return response;
		}

		protected virtual void Log(HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
		{
			Debug.WriteLine("{0} {1} ({2} ms)", request.Method, request.RequestUri, elapsed.TotalMilliseconds);
		}

	}
}
