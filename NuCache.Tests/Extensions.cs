using System;
using System.Net.Http;
using NSubstitute;

namespace NuCache.Tests
{
	public static class Extensions
	{
		public static HttpRequestMessage AsRequest(this Uri self)
		{
			var request = Substitute.For<HttpRequestMessage>();
			request.RequestUri = self;

			return request;
		}
	}
}
