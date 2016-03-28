using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace NuCache
{
	public class Configuration : IConfiguration
	{
		public Uri SourceNugetFeed { get; set; }
		public string CacheDirectory { get; set; }
		public string LogDirectory { get; set; }
		public string StatsFile { get; set; }

		public Configuration()
		{
			var cache = ConfigurationManager.AppSettings["CacheDirectory"];
			var stats = ConfigurationManager.AppSettings["StatsFile"];
			var log = ConfigurationManager.AppSettings["LogDirectory"];
			var feed = ConfigurationManager.AppSettings["SourceNugetFeed"];

			SourceNugetFeed = new Uri(feed);
			StatsFile = RootedPath(stats);
			CacheDirectory = RootedPath(cache);
			LogDirectory = RootedPath(log);

		}

		private string RootedPath(string path)
		{
			return Path.IsPathRooted(path)
				? path
				: Path.Combine(HttpRuntime.AppDomainAppPath, path);
		}
	}
}
