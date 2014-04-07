using System;
using System.Configuration;

namespace NuCache
{
	public class ApplicationSettings
	{
		public virtual string CachePath
		{
			get { return ConfigurationManager.AppSettings["CachePath"]; }
		}

		public virtual Uri RemoteFeed
		{
			get { return new Uri(ConfigurationManager.AppSettings["RemoteFeed"]); }
		}
	}
}
