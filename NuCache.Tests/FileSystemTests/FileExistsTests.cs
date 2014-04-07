using System;
using System.IO;
using Should;
using Should.Core.Assertions;

namespace NuCache.Tests.FileSystemTests
{
	public class FileExistsTests : IDisposable
	{
		private readonly string _filename;
		private readonly FileSystem _fileSystem;

		public FileExistsTests()
		{
			_fileSystem = new FileSystem();

			_filename = Guid.NewGuid().ToString() + ".tmp";
			
			File.Create(_filename).Close();
		}

		public void When_passed_a_relative_path_and_the_file_exists()
		{
			_fileSystem.FileExists(_filename).ShouldBeTrue();
		}

		public void When_passed_a_relative_path_and_the_file_does_not_exist()
		{
			_fileSystem.FileExists("del"+_filename).ShouldBeFalse();
		}

		public void When_passed_an_absolute_path_and_the_file_exists()
		{
			var absolute = Path.GetFullPath(_filename);
			_fileSystem.FileExists(absolute).ShouldBeTrue();
		}

		public void When_passed_an_absolute_path_and_the_file_does_not_exist()
		{
			var absolute = Path.GetFullPath("del" + _filename);
			_fileSystem.FileExists(absolute).ShouldBeFalse();
		}

		public void When_passed_a_blank_filepath()
		{
			_fileSystem.FileExists(string.Empty).ShouldBeFalse();
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
