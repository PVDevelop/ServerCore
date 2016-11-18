using System;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class UserSessionGenerator : IUserSessionGenerator
	{
		public string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
