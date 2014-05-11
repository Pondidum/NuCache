using System.IO;
using Xunit;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class CreateDirectoryTests : BaseFileSystemDirectoryTest
	{
		[Fact]
		public void When_creating_an_existing_directory()
		{
			FileSystem.CreateDirectory(DirectoryName);
		}

		[Fact]
		public void When_creating_a_non_existing_directory()
		{
			Directory.Delete(DirectoryName);
			FileSystem.CreateDirectory(DirectoryName);
		} 
	}
}
