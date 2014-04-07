using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace NuCache.Controllers
{
	public class PackagesController : ApiController
	{
		private readonly IPackageSource _packageSource;

		public PackagesController(IPackageSource source)
		{
			_packageSource = source;
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Get()
		{
			return await _packageSource.Get(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Metadata()
		{
			return await _packageSource.Metadata(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> List()
		{
			return await _packageSource.List(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Search()
		{
			return await _packageSource.Search(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> FindPackagesByID()
		{
			return await _packageSource.FindPackagesByID(Request.RequestUri);
		}

		[HttpGet]
		public async Task<HttpResponseMessage> GetPackageByID()
		{
			return await _packageSource.GetPackageByID(Request.RequestUri);
		}
	}
}
