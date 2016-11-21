using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions
{
	public class InvalidPasswordFormatException : Exception
	{
		public InvalidPasswordFormatException(string email) :
			base($"Password format is invalid for user '{email}'")
		{
		}
	}
}
