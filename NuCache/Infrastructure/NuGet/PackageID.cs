using System;

namespace NuCache.Infrastructure.NuGet
{
	public class PackageID : IEquatable<PackageID>
	{
		public string Name { get; private set; }
		public string Version { get; private set; }

		public PackageID(string name, string version)
		{
			Name = name;
			Version = version;
		}

		public string GetFileName()
		{
			return string.Format("{0}.{1}.nupkg", Name, Version);
		}

		public bool Equals(PackageID other)
		{
			if (other == null) return false;

			if (Name != other.Name) return false;
			if (Version != other.Version) return false;

			return true;
		}
	}
}
