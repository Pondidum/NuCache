using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NuCache
{
	public interface IPackageSource
	{
		Task<HttpResponseMessage> Get(HttpRequestMessage request);
		Task<HttpResponseMessage> Metadata(HttpRequestMessage request);
		Task<HttpResponseMessage> List(HttpRequestMessage request);
		Task<HttpResponseMessage> Search(HttpRequestMessage request);
		Task<HttpResponseMessage> FindPackagesByID(HttpRequestMessage request);
		Task<HttpResponseMessage> GetPackageByID(HttpRequestMessage request);
		Task<HttpResponseMessage> GetPackageIDs(HttpRequestMessage request);
	}
}
