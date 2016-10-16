using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Fake
{
	public class FakeConnectionStringProvider : IConnectionStringProvider
	{
		public string ConnectionString
		{
			get { return null; }
		}
	}
}
