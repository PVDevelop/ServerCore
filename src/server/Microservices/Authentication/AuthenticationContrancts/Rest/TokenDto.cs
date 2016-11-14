using System;

namespace PVDevelop.UCoach.AuthenticationContrancts.Rest
{
	public class TokenDto
	{
		public string Token { get; }
		public DateTime Expiration { get; }

		public TokenDto(string token, DateTime expiration)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));
			if (expiration == default(DateTime)) throw new ArgumentException("Not set", nameof(expiration));
			if (expiration.Kind != DateTimeKind.Utc) throw new ArgumentException("Is not UTC", nameof(expiration));

			Token = token;
			Expiration = expiration;
		}
	}
}
