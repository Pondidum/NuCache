using System.Collections.Generic;
using System.Web.Http.Routing;

namespace NuCache.Models
{
	public class HomeViewModel
	{
		public List<IHttpRoute> Routes { get; set; }

		public HomeViewModel()
		{
			Routes = new List<IHttpRoute>();
		}
	}
}
