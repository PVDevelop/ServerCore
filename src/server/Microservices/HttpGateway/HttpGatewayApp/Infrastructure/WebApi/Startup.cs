using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			HttpGatewayMicroservice.Instance.Container.Populate(services);
			return HttpGatewayMicroservice.Instance.Container.GetInstance<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
		{
			app.UseMiddleware<TestMiddleware>("start");
			app.UseMvc();
			app.UseMiddleware<TestMiddleware>("mvc");
			app.UseExceptionHandler();
			app.UseMiddleware<TestMiddleware>("exception");
			app.UseStaticFiles();
			app.UseMiddleware<TestMiddleware>("static");
			app.UseMiddleware<IndexSelectorMiddleware>();
			app.UseStaticFiles();
		}
	}

	class TestMiddleware
	{
		private readonly string _name;
		private readonly RequestDelegate _next;

		public TestMiddleware(string name, RequestDelegate next)
		{
			_name = name;
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await _next.Invoke(context);
		}
}
}
