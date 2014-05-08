using System;
using System.Net.Http;

namespace NuCache.Infrastructure
{
	public interface IRequestLogger
	{
		void Log(HttpRequestMessage request, HttpResponseMessage response, TimeSpan elapsed);
	}
}
