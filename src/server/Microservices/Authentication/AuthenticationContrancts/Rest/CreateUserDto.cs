using System;

namespace PVDevelop.UCoach.AuthenticationContrancts.Rest
{
	public class CreateUserDto
	{
		public string Email { get; }
		public string Password { get; }

		public CreateUserDto(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			Email = email;
			Password = password;
		}
	}
}
