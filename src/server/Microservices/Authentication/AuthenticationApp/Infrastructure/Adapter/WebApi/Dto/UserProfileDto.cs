using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi.Dto
{
	public class UserProfileDto
	{
		public string Email { get; }

		public UserProfileDto(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));

			Email = email;
		}
	}
}
