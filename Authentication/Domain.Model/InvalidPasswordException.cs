using System;

namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	public class InvalidPasswordException : Exception
	{
		public InvalidPasswordException(string userName) : 
			base($"Password is invalid for user '{userName}'")
		{
		}
	}
}
