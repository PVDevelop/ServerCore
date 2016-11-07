using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public class KeyGeneratorService : IKeyGeneratorService
	{
		public string GenerateConfirmationKey()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
