using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Value-object - токен авторизации пользователя.
	/// </summary>
	public class UserToken
	{
		public string Token { get; }
		public DateTime Expiration { get; }

		public UserToken(string token, DateTime expiration)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));
			if (expiration == default(DateTime)) throw new ArgumentException("Not set", nameof(expiration));
			if (expiration.Kind != DateTimeKind.Utc) throw new ArgumentException("Is not UTC", nameof(expiration));

			Token = token;
			Expiration = expiration;
		}
	}
}
