﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using NuCache;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(Startup))]

namespace NuCache
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.Enrich.FromLogContext()
				.WriteTo.Trace()
				.CreateLogger();


			app.Use(typeof(ProxyingMiddlware));

			app.Run(context =>
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Task.Delay(0);
			});

		}
	}
}
