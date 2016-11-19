using System;
using System.Text;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class ConfirmationKeyGenerator : IConfirmationKeyGenerator
	{
		public string Generate()
		{
			var guidString = Guid.NewGuid().ToString();
			var tokenBytes = Encoding.UTF8.GetBytes(guidString);
			return Convert.ToBase64String(tokenBytes);
		}
	}
}
