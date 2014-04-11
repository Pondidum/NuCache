using System.IO;
using NuCache.Infrastructure;

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
				string.Format("{0}.{1}.nupkg", name, version)
			);
		}

		public void Initialise()
		{
			_fileSystem.CreateDirectory(_settings.CachePath);
		}

		public bool Contains(string name, string version)
		{
			return _fileSystem.FileExists(GetPackagePath(name, version));
		}

		public void Store(string name, string version, Stream contents)
		{
			var path = GetPackagePath(name, version);

			if (_fileSystem.FileExists(path))
			{
				_fileSystem.DeleteFile(path);
			}

			_fileSystem.WriteFile(path, contents);
		}

		public Stream Get(string name, string version)
		{
			var path = GetPackagePath(name, version);

			return _fileSystem.ReadFile(path);
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
