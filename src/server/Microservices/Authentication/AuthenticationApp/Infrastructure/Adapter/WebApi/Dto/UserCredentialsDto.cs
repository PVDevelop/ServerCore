using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi.Dto
{
	public class UserCredentialsDto
	{
		public string Email { get; }
		public string Password { get; }

		public UserCredentialsDto(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			Email = email;
			Password = password;
		}
	}
}
