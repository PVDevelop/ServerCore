using System;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string email) :
			base($"User with email '{email}' not found.")
		{
		}
	}
}
