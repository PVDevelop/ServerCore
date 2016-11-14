using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public class TokenGenerator : ITokenGenerator
	{
		public string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
