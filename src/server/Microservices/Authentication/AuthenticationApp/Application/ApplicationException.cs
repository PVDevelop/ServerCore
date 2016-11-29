using System;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class ApplicationException : Exception
	{
		public ApplicationException(string message) : base(message)
		{
		}
	}
}
