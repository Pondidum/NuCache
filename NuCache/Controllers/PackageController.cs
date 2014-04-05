using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

using NuCache.Infrastructure;

namespace NuCache.Controllers
{
	public class PackagesController : ApiController
	{
		private readonly PackageCache _cache;
		private readonly IPackageRepository _repository;


		public PackagesController(PackageCache cache, IPackageRepository repository)
		{
			_cache = cache;
			_repository = repository;
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Get()
		{
			return await _repository.Get(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Metadata()
		{
			return await _repository.Metadata(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> List()
		{
			return await _repository.List(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Search()
		{
			return await _repository.Search(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> FindPackagesByID()
		{
			return await _repository.FindPackagesByID(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetPackageByID()
		{
			return await _repository.GetPackageByID(Request.RequestUri);
		}
	}
}
