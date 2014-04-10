using System.IO;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class CreateDirectoryTests : BaseFileSystemDirectoryTest
	{
		public void When_creating_an_existing_directory()
		{
			FileSystem.CreateDirectory(DirectoryName);
		}

		public void When_creating_a_non_existing_directory()
		{
			Directory.Delete(DirectoryName);
			FileSystem.CreateDirectory(DirectoryName);
		} 
	}
}
