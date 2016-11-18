using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PVDevelop.UCoach.Configuration;
using StructureMap;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			HttpGatewayMicroservice.Instance.Container.Populate(services);
			return HttpGatewayMicroservice.Instance.Container.GetInstance<IServiceProvider>();
		}

		public void Configure(
			IApplicationBuilder app,
			IHostingEnvironment env,
			ILoggerFactory loggerFactory,
			IApplicationLifetime appLifetime)
		{
			app.
				UseExceptionHandler().
				UseMiddleware<ProxyMiddleware>(HttpGatewayMicroservice.Instance.Container.GetInstance<IConnectionStringProvider>()).
				UseStaticFiles().
				UseMiddleware<UiBinariesSelectorMiddleware>().
				UseStaticFiles();
		}
	}
}
