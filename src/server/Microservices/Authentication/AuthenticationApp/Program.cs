using PVDevelop.UCoach.Microservice;

namespace PVDevelop.UCoach.AuthenticationApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			new ConsoleApplication(AuthenticationMicroservice.Instance).Start();
		}
	}
}
