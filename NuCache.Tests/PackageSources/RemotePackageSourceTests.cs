using System;
using NSubstitute;
using NuCache.Infrastructure;
using NuCache.PackageSources;

namespace NuCache.Tests.PackageSources
{
	public class RemotePackageSourceTests
	{

		private void Test(Action<RemotePackageSource, Uri> method)
		{
			var client = Substitute.For<WebClient>();
			var transformer = new UriHostTransformer(new Uri("http://localhost.fiddler:42174"));

			var source = new RemotePackageSource(client, transformer);

			method(source, new Uri("http://example.com/api/v2"));

			client.Received().GetResponseAsync(new Uri("http://localhost.fiddler:42174/api/v2"));	
		}

		public void When_calling_get()
		{
			Test((s,u) => s.Get(u));
		}

		public void When_calling_metadata()
		{
			Test((s,u) => s.Metadata(u));
		}

		public void When_calling_list()
		{
			Test((s,u) => s.List(u));
		}

		public void When_calling_search()
		{
			Test((s,u) => s.Search(u));
		}

		public void When_calling_findPackagesByID()
		{
			Test((s,u) => s.FindPackagesByID(u));
		}

		public void When_calling_getPackageByID()
		{
			Test((s, u) => s.GetPackageByID(u));
		}
	}
}
