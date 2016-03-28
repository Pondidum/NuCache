using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace NuCache.Tests
{
	public class PackageNameTests
	{
		[Fact]
		public void When_parsing_a_semver_filename_string()
		{
			var package = PackageName.Parse("finite.4.1.0.nupkg");

			package.Name.ShouldBe("finite");
			package.Version.ShouldBe("4.1.0");
			package.ToString().ShouldBe("finite.4.1.0.nupkg");
		}

		[Fact]
		public void When_parsing_a_dotnet_filename_string()
		{
			var package = PackageName.Parse("finite.4.1.0.56.nupkg");

			package.Name.ShouldBe("finite");
			package.Version.ShouldBe("4.1.0.56");
			package.ToString().ShouldBe("finite.4.1.0.56.nupkg");
		}

		[Fact]
		public void When_loading_from_name_and_version()
		{
			var package = new PackageName("finite", "4.1.0");

			package.Name.ShouldBe("finite");
			package.Version.ShouldBe("4.1.0");
			package.ToString().ShouldBe("finite.4.1.0.nupkg");
		}

		[Fact]
		public void When_serialized()
		{
			var dto = new Dto
			{
				Name = PackageName.Parse("finite.4.1.0.nupkg")
			};

			var json = JsonConvert.SerializeObject(dto);

			json.ShouldBe("{\"Name\":\"finite.4.1.0.nupkg\"}");
		}

		[Fact]
		public void When_deserialized()
		{
			var json = "{\"Name\":\"finite.4.1.0.nupkg\"}";
			var dto = JsonConvert.DeserializeObject<Dto>(json);

			dto.Name.ToString().ShouldBe("finite.4.1.0.nupkg");
		}

		private class Dto
		{
			public PackageName Name { get; set; }
		}
	}
}
