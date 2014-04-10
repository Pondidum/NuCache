using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NuCache
{
	public interface IPackageSource
	{
		Task<HttpResponseMessage> Get(Uri request);
		Task<HttpResponseMessage> Metadata(Uri request);
		Task<HttpResponseMessage> List(Uri request);
		Task<HttpResponseMessage> Search(Uri request);
		Task<HttpResponseMessage> FindPackagesByID(Uri request);
		Task<HttpResponseMessage> GetPackageByID(Uri request, string packageID, string version);
	}
}
