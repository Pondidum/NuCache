using System;
using System.Diagnostics;
using System.Net.Http;

namespace NuCache.Infrastructure
{
	public class DebugLogger : IRequestLogger
	{
		public void Log(HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed)
		{
			Debug.WriteLine("{0} {1} ({2} ms)", request.Method, request.RequestUri, elapsed.TotalMilliseconds);
		}
	}
}
