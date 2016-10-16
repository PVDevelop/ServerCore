using System;

namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	public class InvalidEmailFormatException : Exception
	{
		public InvalidEmailFormatException(string email) :
			base($"Email '{email}' has invalid format")
		{
		}
	}
}
