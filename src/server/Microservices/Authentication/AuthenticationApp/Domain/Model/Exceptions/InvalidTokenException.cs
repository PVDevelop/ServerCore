using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions
{
	public class InvalidTokenException : Exception
	{
		public InvalidTokenException(string message) : base(message)
		{
		}
	}
}
