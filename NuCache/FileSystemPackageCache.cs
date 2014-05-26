using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NuCache.Infrastructure;
using NuCache.Infrastructure.NuGet;

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

		public IEnumerable<PackageID> GetAllPackages()
		{
			var packages = new List<PackageID>();
			var removals = new List<string>();

			foreach (var path in _fileSystem.ListDirectory(_settings.CachePath).ToList())
			{
				using (var stream = _fileSystem.ReadFile(path))
				{
					var status = Package.TryLoadPackage(stream);

					if (status.Success)
					{
						packages.Add(status.Target.Metadata.ID);
					}
					else
					{
						removals.Add(path);
					}
				}
			}

			if (removals.Any())
			{
				Task.Run(() => removals.ForEach(path => _fileSystem.DeleteFile(path)));
			}

			return packages;
		}
	}
}
