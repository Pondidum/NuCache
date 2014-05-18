using System;
using System.Net;
using System.Net.Http;

namespace NuCache.Infrastructure.Statistics
{
	public class HttpStatistic
	{
		public Uri Request { get; private set; }
		public String StatusCode { get; private set; }
		public string PackageName { get; set; }
		public string PackageVersion { get; set; }

		public HttpStatistic(Uri request, HttpStatusCode response, string name, string version)
		{
			Request = request;
			StatusCode = response.ToString();
			PackageName = name;
			PackageVersion = version;
		}
	}
}
