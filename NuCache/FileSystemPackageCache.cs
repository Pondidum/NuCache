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

		private string GetPackagePath(PackageID packageID)
		{
			return Path.Combine(_settings.CachePath, packageID.GetFileName());
		}

		public void Initialise()
		{
			_fileSystem.CreateDirectory(_settings.CachePath);
		}

		public bool Contains(PackageID packageID)
		{
			return _fileSystem.FileExists(GetPackagePath(packageID));
		}

		public void Store(PackageID packageID, Stream contents)
		{
			var path = GetPackagePath(packageID);

			if (_fileSystem.FileExists(path))
			{
				_fileSystem.DeleteFile(path);
			}

			_fileSystem.WriteFile(path, contents);
		}

		public Stream Get(PackageID packageID)
		{
			var path = GetPackagePath(packageID);

			return _fileSystem.ReadFile(path);
		}

		public void Remove(PackageID packageID)
		{
			var path = GetPackagePath(packageID);

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
