using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions
{
	public class InvalidPasswordFormatException : Exception
	{
		public InvalidPasswordFormatException() :
			base($"Password format is invalid.")
		{
		}
	}
}
