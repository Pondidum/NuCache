using System.IO;

namespace NuCache
{
	public class FileSystemPackageCache : IPackageCache
	{
		private readonly IFileSystem _fileSystem;
		private readonly ApplicationSettings _settings;

		public FileSystemPackageCache(IFileSystem fileSystem, ApplicationSettings settings)
		{
			_fileSystem = fileSystem;
			_settings = settings;
		}

		private string GetPackagePath(string name, string version)
		{
			return Path.Combine(
				_settings.CachePath, 
				string.Format("{0}.{1}", name, version)
			);
		}

		public bool Contains(string name, string version)
		{
			return _fileSystem.FileExists(GetPackagePath(name, version));
		}

		public void Write(string name, string version, Stream contents)
		{
			var path = GetPackagePath(name, version);

			if (_fileSystem.FileExists(path))
			{
				_fileSystem.DeleteFile(path);
			}

			_fileSystem.WriteFile(path, contents);
		}

		public void Remove(string name, string version)
		{
			var path = GetPackagePath(name, version);

			if (_fileSystem.FileExists(path))
			{
				_fileSystem.DeleteFile(path);
			}
		}

		public void RemoveAll()
		{
			foreach (var path in _fileSystem.ListDirectory(_settings.CachePath))
			{
				_fileSystem.DeleteFile(path);
			}
		}
	}
}
