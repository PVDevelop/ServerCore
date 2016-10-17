using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
using StructureMap.AutoMocking;

namespace PVDevelop.UCoach.Authentication
{
	public class Startup
	{
		private readonly IConfigurationRoot _configurationRoot;
		private readonly IContainer _container;

		public Startup()
		{
			_container = new Container();
			_configurationRoot = 
				new ConfigurationBuilder().
				SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("config.json").
				Build();
		}

		public Startup(ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole();
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			_container.Configure(x =>
			{
				x.For<IUserService>().Use<UserService>();

				SetupUserService(x);
				SetupMongo(x);
				SetupInitializers(x);

				x.Populate(services);
			});

			_container.AssertConfigurationIsValid();

			return _container.GetInstance<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
		{
			app.UseMvc();
			app.UseExceptionHandler();

			appLifetime.ApplicationStarted.Register(StartInitializers);
		}

		private void SetupUserService(ConfigurationExpression x)
		{
			x.For<IKeyGeneratorService>().Use<KeyGeneratorService>();
			x.For<IUtcTimeProvider>().Use<UtcTimeProvider>();
			x.For<IUserRepository>().Use<MongoUserRepository>();
			x.For<IConfirmationRepository>().Use<InMemoryConfirmationRepository>().Singleton();
			x.For<IConfirmationProducer>().Use<FakeConfirmationProducer>();
			x.For<IConfigurationRoot>().Add(_configurationRoot);
		}

		private void SetupMongo(ConfigurationExpression x)
		{
			x.For<IMongoRepository<MongoUser>>().Use<MongoRepository<MongoUser>>();
			x.For<IMongoCollectionVersionValidator>().Use<MongoCollectionVersionValidatorByClassAttribute>();
			x.For<IConnectionStringProvider>().Use<ConnectionStringFromConfigurationRootProvider>().Ctor<string>().Is("Mongo");
		}

		private void SetupInitializers(ConfigurationExpression x)
		{
			x.For<IInitializer>().Use<MongoUserCollectionInitializer>();
		}

		private void StartInitializers()
		{
			foreach (var initializer in _container.GetAllInstances<IInitializer>())
			{
				initializer.Initialize();
			}
		}
	}
}
