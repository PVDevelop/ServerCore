using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	public class NotAuthorizedException : Exception
	{
		public NotAuthorizedException(string message) : base(message)
		{
		}
	}
}
