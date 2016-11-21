using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions
{
	public class InvalidPasswordException : Exception
	{
		public InvalidPasswordException(string userName) : 
			base($"Password is invalid for user '{userName}'.")
		{
		}
	}
}
