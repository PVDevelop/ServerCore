using System;

namespace PVDevelop.UCoach.Domain.Exceptions
{
	public class InvalidPasswordException : Exception
	{
		public InvalidPasswordException(string userName) : 
			base($"Password is invalid for user '{userName}'.")
		{
		}
	}
}
