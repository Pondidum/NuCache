using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NSubstitute;
using NuCache.Infrastructure;
using NuCache.PackageSources;
using NuCache.ProxyBehaviour;
using NuCache.Rewriters;
using Xunit;

namespace NuCache.Tests.PackageSources
{
	public class ProxyingPackageSourceTests
	{
		private readonly HttpRequestMessage _request;
		private readonly WebClient _client;
		private readonly ProxyingPackageSource _source;

		public ProxyingPackageSourceTests()
		{
			_request = new Uri("http://example.com/api/v2").AsRequest();
			_client = Substitute.For<WebClient>();
			_client.GetResponseAsync(Arg.Any<Uri>()).Returns(new Task<HttpResponseMessage>(() => new HttpResponseMessage()));

			var settings = Substitute.For<ApplicationSettings>();

			var transformer = new UriRewriter();
			var behaviours = new ProxyBehaviourSet(new[] { new XmlRewriteBehaviour(new XmlRewriter(transformer)) });
			var cache = Substitute.For<IPackageCache>();

			settings.RemoteFeed.Returns(new Uri("http://localhost.fiddler:42174"));

			_source = new ProxyingPackageSource(settings, _client, behaviours, cache, transformer);
		}

		private void Validate()
		{
			_client.Received().GetResponseAsync(new Uri("http://localhost.fiddler:42174/api/v2"));
		}

		[Fact]
		public void When_calling_get()
		{
			_source.Get(_request);
			Validate();
		}

		[Fact]
		public void When_calling_metadata()
		{
			_source.Metadata(_request);
			Validate();
		}

		[Fact]
		public void When_calling_list()
		{
			_source.List(_request);
			Validate();
		}

		[Fact]
		public void When_calling_search()
		{
			_source.Search(_request);
			Validate();
		}

		[Fact]
		public void When_calling_findPackagesByID()
		{
			_source.FindPackagesByID(_request);
			Validate();
		}

		[Fact]
		public void When_calling_getPackageByID()
		{
			_source.GetPackageByID(_request);//, "elmah", "1.0.0"
			Validate();
		}

		[Fact]
		public void When_calling_get_packageIDs()
		{
			_source.GetPackageIDs(_request);
			Validate();
		}
	}
}
