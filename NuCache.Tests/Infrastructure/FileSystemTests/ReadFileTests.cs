using System.IO;
using Should;
using Xunit;
using Assert = Should.Core.Assertions.Assert;

namespace NuCache.Tests.Infrastructure.FileSystemTests
{
	public class ReadFileTests : BaseFileSystemFileTest
	{
		private const string Contents = "Some test string with !£$%^&*() characters.";

		private string StringFromStream(Stream stream)
		{
			using (var sr = new StreamReader(stream))
			{
				return sr.ReadToEnd();
			}
		}

		[Fact]
		public void When_reading_from_an_existing_file()
		{
			File.WriteAllText(Filename, Contents);

			StringFromStream(FileSystem.ReadFile(Filename)).ShouldEqual(Contents);
		}

		[Fact]
		public void When_reading_from_a_non_existing_file()
		{
			File.Delete(Filename);

			Assert.Throws<FileNotFoundException>(() => FileSystem.ReadFile(Filename));
		}
	}
}
