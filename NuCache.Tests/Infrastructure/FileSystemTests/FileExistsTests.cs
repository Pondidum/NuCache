using System.IO;
using Should;
using Xunit;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class FileExistsTests : BaseFileSystemFileTest
	{
		[Fact]
		public void When_passed_a_relative_path_and_the_file_exists()
		{
			FileSystem.FileExists(Filename).ShouldBeTrue();
		}

		[Fact]
		public void When_passed_a_relative_path_and_the_file_does_not_exist()
		{
			FileSystem.FileExists("del"+Filename).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_an_absolute_path_and_the_file_exists()
		{
			var absolute = Path.GetFullPath(Filename);
			FileSystem.FileExists(absolute).ShouldBeTrue();
		}

		[Fact]
		public void When_passed_an_absolute_path_and_the_file_does_not_exist()
		{
			var absolute = Path.GetFullPath("del" + Filename);
			FileSystem.FileExists(absolute).ShouldBeFalse();
		}

		[Fact]
		public void When_passed_a_blank_filepath()
		{
			FileSystem.FileExists(string.Empty).ShouldBeFalse();
		}
	}
}
