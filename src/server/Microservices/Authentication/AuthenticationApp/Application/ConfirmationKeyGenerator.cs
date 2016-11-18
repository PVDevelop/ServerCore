using System;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class ConfirmationKeyGenerator : IConfirmationKeyGenerator
	{
		public string Generate()
		{
			return Guid.NewGuid().ToString();
		}
	}
}
