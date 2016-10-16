using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PVDevelop.UCoach.Authentication.Application;
using PVDevelop.UCoach.Authentication.Domain.Model;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Authentication.Infrastructure.Fake;
using PVDevelop.UCoach.Authentication.Infrastructure.Mongo;
using PVDevelop.UCoach.Timing;
using PVDevelop.UCoach.Mongo;
using PVDevelop.UCoach.Configuration;
using StructureMap;

namespace PVDevelop.UCoach.Authentication
{
	public class Startup
	{
		public Startup(ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			var container = new Container();
			container.Configure(x =>
			{
				x.For<IUserService>().Use<UserService>();

				SetupUserService(x);
				SetupMongo(x);
				SetupConfiguration(x);

				x.Populate(services);
			});

			container.AssertConfigurationIsValid();

			return container.GetInstance<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			app.UseMvc();
			app.UseExceptionHandler();
		}

		private void SetupUserService(ConfigurationExpression x)
		{
			x.For<IKeyGeneratorService>().Use<KeyGeneratorService>();
			x.For<IUtcTimeProvider>().Use<UtcTimeProvider>();
			x.For<IUserRepository>().Use<MongoUserRepository>();
			x.For<IConfirmationRepository>().Use<InMemoryConfirmationRepository>().Singleton();
			x.For<IConfirmationProducer>().Use<FakeConfirmationProducer>();
		}

		private void SetupMongo(ConfigurationExpression x)
		{
			x.For<IMongoRepository<MongoUser>>().Use<MongoRepository<MongoUser>>();
			x.For<IMongoCollectionVersionValidator>().Use<MongoCollectionVersionValidatorByClassAttribute>();
		}

		private void SetupConfiguration(ConfigurationExpression x)
		{
			x.For<IConnectionStringProvider>().Use<FakeConnectionStringProvider>();
		}
	}
}
