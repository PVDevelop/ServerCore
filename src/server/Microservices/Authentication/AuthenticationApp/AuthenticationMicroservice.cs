using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Email;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.Confirmation;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.User;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.UserSession;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Mongo;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Microservice;
using PVDevelop.UCoach.Timing;
using StructureMap;

namespace PVDevelop.UCoach.AuthenticationApp
{
	public class AuthenticationMicroservice : IMicroservice
	{
		internal static readonly AuthenticationMicroservice Instance = new AuthenticationMicroservice();

		public IContainer Container { get; private set; }
		public IConfigurationRoot ConfigurationRoot { get; private set; }

		private AuthenticationMicroservice()
		{ }

		public void Start(CancellationToken cancellationToken)
		{
			SetupConfigurationRoot();
			SetupContainer();

			StartInitializers();
			StartWebHost(cancellationToken);
		}

		private void SetupConfigurationRoot()
		{
			ConfigurationRoot =
				new ConfigurationBuilder().
				SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("config.json").
				Build();
		}

		private void SetupContainer()
		{
			Container = new Container(x =>
			{
				SetupUserService(x);
				SetupMongo(x);
				SetupInitializers(x);
				SetupConfigurations(x);
			});

			Container.AssertConfigurationIsValid();
		}

		private void SetupUserService(ConfigurationExpression x)
		{
			x.For<IUserService>().Use<UserService>();
			x.For<IConfirmationKeyGenerator>().Use<ConfirmationKeyGenerator>();
			x.For<IUtcTimeProvider>().Use<UtcTimeProvider>();
			x.For<IUserRepository>().Use<MongoUserRepository>();
			x.For<IUserSessionRepository>().Use<MongoUserSessionRepository>();
			x.For<IConfirmationRepository>().Use<MongoConfirmationRepository>();
			x.For<IConfirmationProducer>().Use<EmailConfirmationProducer>();
			x.For<IConfigurationRoot>().Add(ConfigurationRoot);
		}

		private void SetupMongo(ConfigurationExpression x)
		{
			x.For<IMongoRepository<MongoUser>>().Use<MongoRepository<MongoUser>>();
			x.For<IMongoRepository<MongoConfirmation>>().Use<MongoRepository<MongoConfirmation>>();
			x.For<IMongoRepository<MongoUserSession>>().Use<MongoRepository<MongoUserSession>>();
			x.
				For<IConnectionStringProvider>().
				Use<ConnectionStringFromConfigurationRootProvider>().
				Ctor<string>().
				Is("Mongo");
		}

		private void SetupInitializers(ConfigurationExpression x)
		{
			x.For<IInitializer>().Use<MongoUserInitializer>();
			x.For<IInitializer>().Use<MongoConfirmationInitializer>();
			x.For<IInitializer>().Use<MongoUserSessionInitializer>();
		}

		private void SetupConfigurations(ConfigurationExpression x)
		{
			x.
				For<IConfigurationSectionProvider<EmailProducerSettings>>().
				Use<ConfigurationRootConfigurationSectionProvider<EmailProducerSettings>>().
				Ctor<string>().
				Is("EmailConfirmationProducer");
		}

		private void StartInitializers()
		{
			foreach (var initializer in Instance.Container.GetAllInstances<IInitializer>())
			{
				initializer.Initialize();
			}
		}

		private void StartWebHost(CancellationToken cancellationToken)
		{
			var hostAddress = ConfigurationRoot.GetConnectionString("Host");
			new WebHostBuilder().
				UseUrls(hostAddress).
				UseKestrel().
				UseStartup<Startup>().
				Build().
				Run(cancellationToken);
		}

		public void Dispose()
		{
			if (Container != null)
			{
				Container.Dispose();
				Container = null;
			}
		}
	}
}
