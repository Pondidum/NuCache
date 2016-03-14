using System.IO;
using System.Web;

namespace NuCache
{
	public class PackageCache
	{
		private readonly string _directory;

		public PackageCache(string directory)
		{
			_directory = directory;
			Directory.CreateDirectory(_directory);
		}

		public bool Contains(string packageName)
		{
			return File.Exists(Path.Combine(_directory, packageName));
		}

		public byte[] GetPackage(string packageName)
		{
			return File.ReadAllBytes(Path.Combine(_directory, packageName));
		}

		public void StorePackage(string packageName, byte[] contents)
		{
			File.WriteAllBytes(Path.Combine(_directory, packageName), contents);
		}
	}
}
