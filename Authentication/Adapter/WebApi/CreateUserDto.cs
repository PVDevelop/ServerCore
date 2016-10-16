using System;

namespace PVDevelop.UCoach.Authentication.Adapter.WebApi
{
	public class CreateUserDto
	{
		public string Email { get; }
		public string Password { get; }
		public string ConfirmationUrl { get; }

		public CreateUserDto(string email, string password, string confirmationUrl)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));
			if (string.IsNullOrWhiteSpace(confirmationUrl)) throw new ArgumentException("Not set", nameof(confirmationUrl));

			Email = email;
			Password = password;
			ConfirmationUrl = confirmationUrl;
		}
	}
}
