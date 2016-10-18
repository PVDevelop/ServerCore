using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PVDevelop.UCoach.Authentication.Application;
using PVDevelop.UCoach.Authentication.Domain.Model;
using PVDevelop.UCoach.Authentication.Infrastructure;
using PVDevelop.UCoach.Authentication.Infrastructure.Email;
using PVDevelop.UCoach.Authentication.Infrastructure.Mongo;
using StructureMap;
using PVDevelop.UCoach.Timing;
using PVDevelop.UCoach.Mongo;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;

namespace PVDevelop.UCoach.Authentication
{
	public class Startup
	{
		private readonly IConfigurationRoot _configurationRoot;
		private readonly IContainer _container;

		public Startup(IHostingEnvironment env)
		{
			_container = new Container();
			_configurationRoot =
				new ConfigurationBuilder().
				SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("config.json").
				AddJsonFile($"config.{env.EnvironmentName}.json", optional: true).
				Build();
		}

		public Startup(ILoggerFactory loggerFactory)
		{
			LoggerHelper.UseLogger(loggerFactory);
		}

		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddOptions();
			services.Configure<EmailProducerSettings>(_configurationRoot.GetSection("EmailConfirmationProducer"));
			services.AddMvc();

			_container.Configure(x =>
			{
				x.For<IUserService>().Use<UserService>();

				SetupUserService(x);
				SetupMongo(x);
				SetupInitializers(x);
				SetupValidators(x);

				x.Populate(services);
			});

			_container.AssertConfigurationIsValid();

			return _container.GetInstance<IServiceProvider>();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
		{
			app.UseMvc();
			app.UseExceptionHandler();

			appLifetime.ApplicationStarted.Register(OnStarted);
		}

		private void OnStarted()
		{
			StartInitializers();
			StartValidators();
		}

		private void SetupUserService(ConfigurationExpression x)
		{
			x.For<IKeyGeneratorService>().Use<KeyGeneratorService>();
			x.For<IUtcTimeProvider>().Use<UtcTimeProvider>();
			x.For<IUserRepository>().Use<MongoUserRepository>();
			x.For<IConfirmationRepository>().Use<MongoConfirmationRepository>();
			x.For<IConfirmationProducer>().Use<EmailConfirmationProducer>();
			x.For<IConfigurationRoot>().Add(_configurationRoot);
		}

		private void SetupMongo(ConfigurationExpression x)
		{
			x.For<IMongoRepository<MongoUser>>().Use<MongoRepository<MongoUser>>();
			x.For<IMongoRepository<MongoConfirmation>>().Use<MongoRepository<MongoConfirmation>>();
			x.For<IConnectionStringProvider>().Use<ConnectionStringFromConfigurationRootProvider>().Ctor<string>().Is("Mongo");
		}

		private void SetupInitializers(ConfigurationExpression x)
		{
			x.For<IInitializer>().Use<MongoUserRepository>();
			x.For<IInitializer>().Use<MongoConfirmationRepository>();
		}

		private void SetupValidators(ConfigurationExpression x)
		{
			x.For<IValidator>().Use<MongoUserRepository>();
			x.For<IValidator>().Use<MongoConfirmationRepository>();
		}

		private void StartInitializers()
		{
			foreach (var initializer in _container.GetAllInstances<IInitializer>())
			{
				initializer.Initialize();
			}
		}

		private void StartValidators()
		{
			foreach (var validator in _container.GetAllInstances<IValidator>())
			{
				validator.Validate();
			}
		}
	}
}
