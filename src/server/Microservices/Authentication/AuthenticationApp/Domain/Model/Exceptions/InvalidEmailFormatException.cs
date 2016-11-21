using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions
{
	public class InvalidEmailFormatException : Exception
	{
		public InvalidEmailFormatException(string email) :
			base($"Email '{email}' has invalid format")
		{
		}
	}
}
