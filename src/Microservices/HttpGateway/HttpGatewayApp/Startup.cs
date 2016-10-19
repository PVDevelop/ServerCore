using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Rest;

namespace PVDevelop.UCoach.HttpGatewayApp
{
	public class Startup
	{
		private readonly IContainer _container;
		private readonly IConfigurationRoot _configuration;

		public Startup(IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			_container = new Container();

			LoggerHelper.UseLogger(loggerFactory);

			_configuration = new ConfigurationBuilder().
				SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("config.json").
				AddJsonFile($"config.{env.EnvironmentName}.json", optional: true).
				Build();
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			_container.Configure(x =>
			{
				x.Populate(services);
				x.For<IConfigurationRoot>().Add(_configuration);
			});

			_container.AssertConfigurationIsValid();

			return _container.GetInstance<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
		{
			app.UseMvc();
			app.UseExceptionHandler();
		}
	}
}
