using System;
using NSubstitute;
using NuCache.Infrastructure;
using NuCache.PackageSources;

namespace NuCache.Tests.PackageSources
{
	public class RemotePackageSourceTests
	{
		public void When_calling_get()
		{
			var client = Substitute.For<WebClient>();
			var transformer = new UriHostTransformer(new Uri("http://localhost.fiddler:42174"));

			var source = new RemotePackageSource(client, transformer);

			source.Get(new Uri("http://example.com/api/v2"));

			client.Received().GetResponseAsync(new Uri("http://localhost.fiddler:42174/api/v2"));
		}
	}
}
