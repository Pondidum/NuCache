using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace NuCache
{
	public class ApplicationSettings
	{
		private readonly Lazy<String> _cachePath;
		private readonly Lazy<String> _statisticsPath;

		public ApplicationSettings()
		{

			var rootPath = new Func<string, string>(path =>
				Path.IsPathRooted(path) 
					? path 
					: Path.Combine(HttpRuntime.AppDomainAppPath, path)
			);

			_cachePath = new Lazy<string>(() => rootPath(ConfigurationManager.AppSettings["CachePath"]));
			_statisticsPath = new Lazy<string>(() => rootPath(ConfigurationManager.AppSettings["StatisticsPath"]));
		}
		
		public virtual string CachePath
		{
			get { return _cachePath.Value; }
		}

		public virtual String StatisticsPath
		{
			get { return _statisticsPath.Value; }
		}

		public virtual Uri RemoteFeed
		{
			get { return new Uri(ConfigurationManager.AppSettings["RemoteFeed"]); }
		}
	}
}
