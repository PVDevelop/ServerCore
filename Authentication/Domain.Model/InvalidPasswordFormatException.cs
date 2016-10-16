using System;

namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	public class InvalidPasswordFormatException : Exception
	{
		public InvalidPasswordFormatException(string email) :
			base($"Password format is invalid for user '{email}'")
		{
		}
	}
}
