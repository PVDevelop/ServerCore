using System;

namespace PVDevelop.UCoach.Domain.Exceptions
{
	public class InvalidPasswordFormatException : Exception
	{
		public InvalidPasswordFormatException() :
			base($"Password format is invalid.")
		{
		}
	}
}
