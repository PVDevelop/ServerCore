using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions
{
	public class AlreadyConfirmedException : Exception
	{
		public AlreadyConfirmedException(string userId) :
			base($"User with id '{userId}' already confirmed.")
		{
		}
	}
}
