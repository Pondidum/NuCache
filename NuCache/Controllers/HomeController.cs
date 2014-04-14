using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;
using NuCache.Models;
using Spark;
using Spark.FileSystem;

namespace NuCache.Controllers
{
	public class HomeController : ApiController
	{
		[System.Web.Http.HttpGet]
		public HttpResponseMessage GetIndex()
		{
			var view = new SparkViewDescriptor();
			view.AddTemplate("Home.spark");

			var engine = new Spark.SparkViewEngine();
			engine.DefaultPageBaseType = typeof(NuCacheView).FullName;

			var entry = engine.CreateEntry(view);
			
			var instance = (NuCacheView<HomeViewModel>) entry.CreateInstance();
			instance.Model = new HomeViewModel { Name = "Andy"};

			var content = new PushStreamContent((responseStream, cont, context) =>
			{
				using (var writer = new StreamWriter(responseStream))
				{
					instance.RenderView(writer);
				}
				responseStream.Close();
			});

			var response = new HttpResponseMessage(HttpStatusCode.OK);
			response.Content = content;
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

			return response;
		}

	}

	public abstract class NuCacheView : SparkViewBase
	{
		
	}

	public class NuCacheView<TViewModel> : NuCacheView where TViewModel : class
	{
		public TViewModel Model { get; set; }

		public override void Render()
		{
			RenderView(Output);
		}

		public override Guid GeneratedViewId
		{
			get { return Guid.NewGuid(); }
		}
	}
}