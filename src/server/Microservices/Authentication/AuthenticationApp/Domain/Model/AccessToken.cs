using System;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Value-object - токен доступа.
	/// </summary>
	public class AccessToken
	{
		public string UserId { get; }
		public string Token { get; }
		public DateTime Expiration { get; }

		public AccessToken(string userId, string token, DateTime expiration)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));
			if (expiration == default(DateTime)) throw new ArgumentException("Not set", nameof(expiration));
			if (expiration.Kind != DateTimeKind.Utc) throw new ArgumentException("Is not UTC", nameof(expiration));

			UserId = userId;
			Token = token;
			Expiration = expiration;
		}
	}
}
