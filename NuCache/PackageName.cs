using System.Text.RegularExpressions;

namespace NuCache
{
	public class PackageName
	{
		public string Name { get; }
		public string Version { get; }

		public static PackageName Parse(string fileName)
		{
			var expression = new Regex(@"^(?<name>.*?)\.(?<version>(\d*\.){2,3}\d*)\.");
			var match = expression.Match(fileName);

			var name = match.Groups["name"].Value;
			var version = match.Groups["version"].Value;

			return new PackageName(name, version);
		}

		public PackageName(string name, string version)
		{
			Name = name.Trim();
			Version = version.Trim();
		}

		public override string ToString()
		{
			return $"{Name}.{Version}.nupkg";
		}
	}
}
