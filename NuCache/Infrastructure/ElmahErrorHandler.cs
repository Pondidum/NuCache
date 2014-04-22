using System.Web;
using System.Web.Http.Filters;
using Elmah;

namespace NuCache.Infrastructure
{
	public class ElmahErrorHandler : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext context)
		{
			ErrorLog.GetDefault(HttpContext.Current).Log(new Error(context.Exception));
		}
	}
}
