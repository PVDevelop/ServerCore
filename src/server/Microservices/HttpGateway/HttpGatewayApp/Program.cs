using PVDevelop.UCoach.Microservice;

namespace PVDevelop.UCoach.HttpGatewayApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new ConsoleApplication(HttpGatewayMicroservice.Instance).Start();
		}
	}
}
