using System;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class ConfirmationNotFoundException : Exception
	{
		public ConfirmationNotFoundException(string confirmationKey) :
			base($"Confirmation '{confirmationKey}' not found.")
		{
		}
	}
}
