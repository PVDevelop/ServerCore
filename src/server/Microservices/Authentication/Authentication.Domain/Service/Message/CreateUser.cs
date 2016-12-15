using System;

namespace PVDevelop.UCoach.Domain.Service.Message
{
	public class CreateUser
	{
		public string Email { get; }
		public string Password { get; }

		public CreateUser(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentException("Not set", nameof(password));

			Email = email;
			Password = password;
		}
	}
}
