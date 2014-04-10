using System;
using System.Linq;
using System.Net.Http;
using NSubstitute;
using NuCache.ProxyBehaviour;
using Should.Core.Assertions;

namespace NuCache.Tests.ProxyBehaviour
{
	public class ProxyBehaviourSetTests
	{

		public void When_there_are_no_behaviours()
		{
			var set = new ProxyBehaviourSet(Enumerable.Empty<IProxyBehaviour>());

			Assert.DoesNotThrow(() => set.Execute(new Uri("http://example.com"), new HttpResponseMessage()));
		}

		public void When_there_are_behaviours()
		{
			var b1 = Substitute.For<IProxyBehaviour>();
			var b2 = Substitute.For<IProxyBehaviour>();

			var set = new ProxyBehaviourSet(new[] { b1, b2 });

			var message = Substitute.For<HttpResponseMessage>();
			var url = new Uri("http://example.com");

			set.Execute(url, message);

			b1.Received().Execute(url, message);
			b2.Received().Execute(url, message);
		}

	}
}
