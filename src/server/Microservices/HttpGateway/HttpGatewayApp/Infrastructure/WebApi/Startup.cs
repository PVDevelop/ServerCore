using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
			app.UseMvc();
			app.UseExceptionHandler();
			app.UseStaticFiles();
			app.UseMiddleware<IndexSelectorMiddleware>();
			app.UseStaticFiles();
		}
	}
}
