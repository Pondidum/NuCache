using System.IO;
using Should;

namespace NuCache.Tests.FileSystemTests
{
	public class DirectoryExistsTests : BaseFileSystemDirectoryTest
	{
		public void When_passed_a_blank_directory_path()
		{
			FileSystem.DirectoryExists(string.Empty).ShouldBeFalse();
		}

		public void When_passed_a_relative_directory_path_and_the_directory_doesnt_exist()
		{
			Directory.Delete(DirectoryName);

			FileSystem.DirectoryExists(DirectoryName).ShouldBeFalse();
		}

		public void When_passed_a_relative_directory_path_and_the_directory_exists()
		{
			FileSystem.DirectoryExists(DirectoryName).ShouldBeTrue();
		}

		public void When_passed_an_absolute_directory_path_and_the_directory_doesnt_exist()
		{
			Directory.Delete(DirectoryName);

			var absolute = Path.GetFullPath(DirectoryName);
			FileSystem.DirectoryExists(absolute).ShouldBeFalse();
		}

		public void When_passed_an_absolute_directory_path_and_the_directory_exists()
		{
			var absolute = Path.GetFullPath(DirectoryName);
			FileSystem.DirectoryExists(absolute).ShouldBeTrue();
		}
	}
}
