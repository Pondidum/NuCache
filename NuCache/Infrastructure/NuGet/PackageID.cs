using System;
using System.Linq;

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

		public override int GetHashCode()
		{
			return GetFileName().GetHashCode();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as PackageID);
		}


		public static PackageID FromPackageIDRequest(Uri url)
		{
			var segments = url
				.Segments
				.Where(s => s != "/")
				.Select(s => s.Trim('/'))
				.Reverse()
				.ToList();

			if (segments.First().Contains("("))
			{
				return FromQuery(segments.First());
			}
			else
			{
				var version = segments.FirstOrDefault();
				var name = segments.Skip(1).FirstOrDefault();

				return new PackageID(name, version);
			}
		}

		private static PackageID FromQuery(string query)
		{
			query = query
				.Substring(query.IndexOf("(", StringComparison.OrdinalIgnoreCase))
				.Trim('(', ')');

			var parts = query
				.Split(',')
				.Select(s => s.Split('='))
				.ToDictionary(p => p.First(), p => p.Last());

			var name = parts.FirstOrDefault(p => p.Key.Equals("id", StringComparison.OrdinalIgnoreCase)).Value;
			var version = parts.FirstOrDefault(p => p.Key.Equals("version", StringComparison.OrdinalIgnoreCase)).Value;

			return new PackageID(name.Trim('\''), version.Trim('\''));
		}
	}
}
