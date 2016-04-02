using System.IO;

namespace NuCache
{
	public class PackageCache
	{
		private readonly string _directory;

		public PackageCache(string directory)
		{
			_directory = directory;
		}

		public bool Contains(PackageName packageName)
		{
			return File.Exists(Path.Combine(_directory, packageName.ToString()));
		}

		public byte[] GetPackage(PackageName packageName)
		{
			return File.ReadAllBytes(Path.Combine(_directory, packageName.ToString()));
		}

		public void StorePackage(PackageName packageName, byte[] contents)
		{
			File.WriteAllBytes(Path.Combine(_directory, packageName.ToString()), contents);
		}

		public void RemovePackage(PackageName packageName)
		{
			File.Delete(Path.Combine(_directory, packageName.ToString()));
		}
	}
}
