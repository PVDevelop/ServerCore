using System;

namespace PVDevelop.UCoach.Domain.Exceptions
{
	public class UserWaitingForCreationConfirmException : Exception
	{
		public UserWaitingForCreationConfirmException(string email) :
			base($"User '{email}' is waiting for creation confirm.")
		{
		}
	}
}
