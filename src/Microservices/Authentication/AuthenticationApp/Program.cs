using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace PVDevelop.UCoach.AuthenticationApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("config.json", optional: false)
				.Build();

			var hostAddress = config.GetConnectionString("Host");

			new WebHostBuilder().
				UseUrls(hostAddress).
				UseKestrel().
				UseStartup<Startup>().
				Build().
				Run();
		}
	}
}
