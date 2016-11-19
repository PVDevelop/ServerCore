using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi;
using PVDevelop.UCoach.Microservice;
using StructureMap;

namespace PVDevelop.UCoach.HttpGatewayApp
{
	public class HttpGatewayMicroservice : IMicroservice
	{
		internal static readonly HttpGatewayMicroservice Instance = new HttpGatewayMicroservice();

		public IContainer Container { get; private set; }
		public IConfigurationRoot ConfigurationRoot { get; private set; }

		private HttpGatewayMicroservice()
		{ }

		public void Start(CancellationToken cancellationToken)
		{
			SetupConfigurationRoot();
			SetupContainer();

			StartWebHost();
		}

		private void SetupConfigurationRoot()
		{
			ConfigurationRoot = new ConfigurationBuilder().
				SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("config.json").
				Build();
		}

		private void SetupContainer()
		{
			Container = new Container(x =>
			{
				x.For<IConfigurationRoot>().Use(ConfigurationRoot);
				x.
					For<IConnectionStringProvider>().
					Use<ConnectionStringFromConfigurationRootProvider>().
					Ctor<string>().
					Is("AuthenticationUrl");
			});

			Container.AssertConfigurationIsValid();
		}

		private void StartWebHost()
		{
			var hostAddress = ConfigurationRoot.GetConnectionString("Host");

			new WebHostBuilder().
				UseUrls(hostAddress).
				UseKestrel().
				UseContentRoot(Directory.GetCurrentDirectory()).
				UseStartup<Startup>().
				Build().
				Run();
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
