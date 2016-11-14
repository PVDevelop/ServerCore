using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public class ConfirmationKeyGenerator : IConfirmationKeyGenerator
	{
		public string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
