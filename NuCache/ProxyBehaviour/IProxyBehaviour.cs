using System;
using System.Net.Http;

namespace NuCache.ProxyBehaviour
{
	public interface IProxyBehaviour
	{
		void Execute(HttpRequestMessage request, HttpResponseMessage response);
	}
}
