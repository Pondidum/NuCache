using System;
using System.IO;
using Should;

namespace NuCache.Tests.FileSystemTests
{
	public class WriteFileTests: IDisposable
	{
		private const string Contents = "Some test string with !£$%^&*() characters.";

		private readonly string _filename;
		private readonly FileSystem _fileSystem;

		public WriteFileTests()
		{
			_fileSystem = new FileSystem();

			_filename = Guid.NewGuid().ToString() + ".tmp";
			File.Create(_filename).Close();
		}

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
				_fileSystem.WriteFile(_filename, stream);
			}

			File.ReadAllText(_filename).ShouldEqual(Contents);
		}

		public void When_writing_a_non_existing_file()
		{
			File.Delete(_filename);

			using (var stream = StreamFromString(Contents))
			{
				_fileSystem.WriteFile(_filename, stream);
			}

			File.ReadAllText(_filename).ShouldEqual(Contents);
		}

		public void Dispose()
		{
			try
			{
				if (File.Exists(_filename))
				{
					File.Delete(_filename);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Enable to delete '{0}'", _filename);
			}

		}
	}
}
