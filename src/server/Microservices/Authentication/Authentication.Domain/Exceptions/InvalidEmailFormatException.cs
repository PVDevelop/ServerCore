using System;

namespace PVDevelop.UCoach.Domain.Exceptions
{
	public class InvalidEmailFormatException : Exception
	{
		public InvalidEmailFormatException(string email) :
			base($"Email '{email}' has invalid format")
		{
		}
	}
}
