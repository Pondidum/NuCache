using System.IO;
using Should;

namespace NuCache.Tests.FileSystemTests
{
	public class DeleteFileTests : BaseFileSystemFileTest
	{
		public void When_deleting_an_existing_file_by_relative_path()
		{
			FileSystem.DeleteFile(Filename);

			File.Exists(Filename).ShouldBeFalse();
		}

		public void When_deleting_an_existing_file_by_absolute_path()
		{
			var absolute = Path.GetFullPath(Filename);
			FileSystem.DeleteFile(absolute);

			File.Exists(Filename).ShouldBeFalse();
		}

		public void When_deleting_a_non_existing_file_by_relative_path()
		{
			File.Delete(Filename);
			FileSystem.DeleteFile(Filename);

			File.Exists(Filename).ShouldBeFalse();
		}

		public void When_deleting_a_non_existing_file_by_absolute_path()
		{
			File.Delete(Filename);

			var absolute = Path.GetFullPath(Filename);
			FileSystem.DeleteFile(absolute);

			File.Exists(Filename).ShouldBeFalse();
		}
	}
}
