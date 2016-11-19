using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi
{
	public class Startup
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			AuthenticationMicroservice.Instance.Container.Populate(services);
			return AuthenticationMicroservice.Instance.Container.GetInstance<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseMvc();
			app.UseExceptionHandler();
		}
	}
}
