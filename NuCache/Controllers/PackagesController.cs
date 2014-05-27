using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace NuCache.Controllers
{
	public class PackagesController : ApiController
	{
		private readonly IPackageSource _packageSource;
		private readonly IDictionary<Func<String, bool>, Func<HttpRequestMessage, Task<HttpResponseMessage>>> _dispatchers;

		public PackagesController(IPackageSource source)
		{
			_packageSource = source;

			_dispatchers = new Dictionary<Func<string, bool>, Func<HttpRequestMessage, Task<HttpResponseMessage>>>();

			var ignore = StringComparison.OrdinalIgnoreCase;
			Func<string, string, bool> equals = (one, two) => string.Equals(one, two, ignore);

			_dispatchers.Add(
				u => string.IsNullOrWhiteSpace(u), 
				r => _packageSource.Get(r));

			_dispatchers.Add(
				u => equals(u, "$metadata"),
				r => _packageSource.Metadata(r));

			_dispatchers.Add(
				u => u.StartsWith("packages", ignore),
				r => _packageSource.List(r));

			_dispatchers.Add(
				u => u.StartsWith("FindPackagesByID()"),
				r => _packageSource.FindPackagesByID(r));

			_dispatchers.Add(
				u => u.StartsWith( "search()", ignore),
				r => _packageSource.Search(r));

			_dispatchers.Add(
				u => u.StartsWith("package", ignore),
				r => _packageSource.GetPackageByID(r));

			_dispatchers.Add(
				u => equals(u, "package-ids"),
				r => _packageSource.GetPackageIDs(r));
		}

		[HttpGet]
		public async Task<HttpResponseMessage> Dispatch(string url)
		{
			var dispatcher = _dispatchers.FirstOrDefault(d => d.Key(url)).Value;

			if (dispatcher == null)
			{
				return null;
			}
			
			return await dispatcher(Request);

		}

	}
}
