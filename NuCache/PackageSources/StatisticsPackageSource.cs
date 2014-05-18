using System;
using System.Net.Http;
using System.Threading.Tasks;
using NuCache.Infrastructure.Statistics;

namespace NuCache.PackageSources
{
	public class StatisticsPackageSource : IPackageSource
	{
		private readonly IPackageSource _inner;
		private readonly StatisticsCollector _statisticsCollector;

		public StatisticsPackageSource(IPackageSource inner, StatisticsCollector statisticsCollector)
		{
			_inner = inner;
			_statisticsCollector = statisticsCollector;
		}

		public async Task<HttpResponseMessage> Get(HttpRequestMessage request)
		{
			return await _inner.Get(request);
		}

		public async Task<HttpResponseMessage> Metadata(HttpRequestMessage request)
		{
			return await _inner.Metadata(request);
		}

		public async Task<HttpResponseMessage> List(HttpRequestMessage request)
		{
			return await _inner.List(request);
		}

		public async Task<HttpResponseMessage> Search(HttpRequestMessage request)
		{
			return await _inner.Search(request);
		}

		public async Task<HttpResponseMessage> FindPackagesByID(HttpRequestMessage request)
		{
			return await _inner.FindPackagesByID(request);
		}

		public async Task<HttpResponseMessage> GetPackageByID(HttpRequestMessage request, string name, string version)
		{
			var response = await _inner.GetPackageByID(request, name, version);

			_statisticsCollector.Log(request, response, name, version);

			return response;
		}

		public async Task<HttpResponseMessage> GetPackageIDs(HttpRequestMessage request)
		{
			return await _inner.GetPackageIDs(request);
		}
	}
}
