using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace NuCache
{
	public class ApplicationSettings
	{
		private readonly Lazy<String> _cachePath;

		public ApplicationSettings()
		{
			_cachePath = new Lazy<string>(() =>
			{
				var path = ConfigurationManager.AppSettings["CachePath"];

				if (Path.IsPathRooted(path) == false)
				{
					path = Path.Combine(HttpRuntime.AppDomainAppPath, path);
				}

				return path;
			});
		}

		public virtual string CachePath
		{
			get { return _cachePath.Value; }
		}

		public virtual Uri RemoteFeed
		{
			get { return new Uri(ConfigurationManager.AppSettings["RemoteFeed"]); }
		}
	}
}
