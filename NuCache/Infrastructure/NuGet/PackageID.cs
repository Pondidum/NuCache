namespace NuCache
{
	public class PackageID
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
	}
}
