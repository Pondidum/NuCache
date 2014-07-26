using System.Net.Http;
using NSubstitute;
using NuCache.Controllers;
using Xunit;

namespace NuCache.Tests.Controllers
{
	public class PackagesControllerTests
	{
		private readonly PackagesController _controller;
		private readonly IPackageSource _source;

		public PackagesControllerTests()
		{
			_source = Substitute.For<IPackageSource>();
			_controller = new PackagesController(_source);
		}

		[Fact]
		public void When_no_path_is_requested()
		{
			_controller.Dispatch(null);
			_source.Received().Get(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_an_empty_path_is_requested()
		{
			_controller.Dispatch("");
			_source.Received().Get(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_metadata_is_requested()
		{
			_controller.Dispatch("$metadata");
			_source.Received().Metadata(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_metadata_is_requested_different_case()
		{
			_controller.Dispatch("$METAdata");
			_source.Received().Metadata(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_a_package_list_is_requested()
		{
			_controller.Dispatch("Packages(Id='Aspose.Words',Version='11.1.0')");
			_source.Received().List(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_a_package_list_is_requested_different_case()
		{
			_controller.Dispatch("PACKAges(Id='Aspose.Words',Version='11.1.0')");
			_source.Received().List(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_search_count_is_requested()
		{
			_controller.Dispatch("Search()/$count?$filter=IsLatestVersion&searchTerm=''&targetFramework='net45'&includePrerelease=false");
			_source.Received().Search(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_search_count_is_requested_different_case()
		{
			_controller.Dispatch("SEARCH()/$count?$filter=IsLatestVersion&searchTerm=''&targetFramework='net45'&includePrerelease=false");
			_source.Received().Search(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_search_filter_is_requested()
		{
			_controller.Dispatch("Search()?$filter=IsLatestVersion&$orderby=DownloadCount%20desc,Id&$skip=0&$top=30&searchTerm=''&targetFramework='net45'&includePrerelease=false");
			_source.Received().Search(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_search_filter_is_requested_different_case()
		{
			_controller.Dispatch("SEARCH()?$filter=IsLatestVersion&$orderby=DownloadCount%20desc,Id&$skip=0&$top=30&searchTerm=''&targetFramework='net45'&includePrerelease=false");
			_source.Received().Search(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_a_package_is_downloaded()
		{
			_controller.Dispatch("package/jQuery/2.1.0/");
			_source.Received().GetPackageByID(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_a_package_is_downloaded_different_case()
		{
			_controller.Dispatch("PACKAGE/jQuery/2.1.0/");
			_source.Received().GetPackageByID(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_auto_complete_packages_are_requested()
		{
			_controller.Dispatch("package-ids?partialId=aspose.w");
			_source.Received().GetPackageIDs(Arg.Any<HttpRequestMessage>());
		}

		[Fact]
		public void When_auto_complete_packages_are_requested_different_case()
		{
			_controller.Dispatch("PACKAGE-ids?partialId=aspose.w");
			_source.Received().GetPackageIDs(Arg.Any<HttpRequestMessage>());
		}
	}
}
