using System;
using System.IO;
using Should;

namespace NuCache.Tests.FileSystemTests
{
	public class WriteFileTests: FileSystemTestBase
	{
		private const string Contents = "Some test string with !£$%^&*() characters.";

		private Stream StreamFromString(string input)
		{
			var ms = new MemoryStream();
			var sw = new StreamWriter(ms);
			
			sw.Write(input);
			sw.Flush();
			ms.Position = 0;

			return ms;
		}

		public void When_writing_to_an_existing_file()
		{
			using (var stream = StreamFromString(Contents))
			{
				FileSystem.WriteFile(Filename, stream);
			}

			File.ReadAllText(Filename).ShouldEqual(Contents);
		}

		public void When_writing_a_non_existing_file()
		{
			File.Delete(Filename);

			using (var stream = StreamFromString(Contents))
			{
				FileSystem.WriteFile(Filename, stream);
			}

			File.ReadAllText(Filename).ShouldEqual(Contents);
		}
	}
}
