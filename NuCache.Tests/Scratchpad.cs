using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Testing;
using NuCache.Properties;
using Owin;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace NuCache.Tests
{
	public class Scratchpad
	{
		private readonly ITestOutputHelper _output;

		public Scratchpad(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public async void When_testing_something()
		{
			Func<IOwinContext, Func<Task>, Task> appHandler = (context, next) =>
			{
				_output.WriteLine(context.Request.Uri.ToString());
				context.Response.StatusCode = (int)HttpStatusCode.OK;
				context.Response.ContentType = "text/plain";
				return context.Response.WriteAsync(context.Request.Path.ToString());
			};

			var api = new Startup();
			var host = TestServer.Create(api.Configuration);


			//https://api.nuget.org/
			var result = await host.CreateRequest("v3/registration1/finite/index.json").GetAsync();
			//var result = await host.CreateRequest("v3/index.json").GetAsync();

			result.IsSuccessStatusCode.ShouldBe(true);

			_output.WriteLine(await result.Content.ReadAsStringAsync());

			host.Dispose();
		} 
	}
}
