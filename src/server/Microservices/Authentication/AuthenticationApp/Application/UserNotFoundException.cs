using System;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class UserNotFoundException : Exception
	{
		public UserNotFoundException(string name) :
			base($"User '{name}' not found.")
		{
		}
	}
}
