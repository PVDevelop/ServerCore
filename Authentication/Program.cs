using Microsoft.AspNetCore.Hosting;

namespace PVDevelop.UCoach.Authentication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new WebHostBuilder().
				UseKestrel().
				UseStartup<Startup>().
				Build().
				Run();
		}
	}
}
