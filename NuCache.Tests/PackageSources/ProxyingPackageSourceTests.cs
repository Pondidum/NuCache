﻿using System;
using System.Linq;
using NSubstitute;
using NuCache.Infrastructure;
using NuCache.PackageSources;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;

namespace NuCache.Tests.PackageSources
{
	public class ProxyingPackageSourceTests
	{
		private void Test(Action<ProxyingPackageSource, Uri> method)
		{
			var settings = Substitute.For<ApplicationSettings>();
			var client = Substitute.For<WebClient>();
			var transformer = new UriRewriter();
			var behaviours = new ProxyBehaviourSet(new[] { new XmlRewriteBehaviour(new XmlRewriter(Enumerable.Empty<IXElementTransform>(), transformer)) });
			var cache = Substitute.For<IPackageCache>();

			settings.RemoteFeed.Returns(new Uri("http://localhost.fiddler:42174"));

			var source = new ProxyingPackageSource(settings, client, behaviours, cache, transformer);

			method(source, new Uri("http://example.com/api/v2"));

			client.Received().GetResponseAsync(new Uri("http://localhost.fiddler:42174/api/v2"));
		}

		public void When_calling_get()
		{
			Test((s, u) => s.Get(u));
		}

		public void When_calling_metadata()
		{
			Test((s, u) => s.Metadata(u));
		}

		public void When_calling_list()
		{
			Test((s, u) => s.List(u));
		}

		public void When_calling_search()
		{
			Test((s, u) => s.Search(u));
		}

		public void When_calling_findPackagesByID()
		{
			Test((s, u) => s.FindPackagesByID(u));
		}

		public void When_calling_getPackageByID()
		{
			Test((s, u) => s.GetPackageByID(u, "elmah", "1.0.0"));
		}

		public void When_calling_get_packageIDs()
		{
			Test((s,u) => s.GetPackageIDs(u));
		}
	}
}
