using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public class UserSessionGenerator : IUserSessionGenerator
	{
		public string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
