using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	public class AlreadyConfirmedException : Exception
	{
		public AlreadyConfirmedException(string userId) :
			base($"User with id '{userId}' already confirmed.")
		{
		}
	}
}
