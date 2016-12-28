using System;

namespace PVDevelop.UCoach.Domain.Model
{
	/// <summary>
	/// Токен доступа пользователя.
	/// </summary>
	public class UserAccessToken
	{
		public UserId UserId { get; }
		public string Token { get; }
		public DateTime Expiration { get; }

		public UserAccessToken(UserId userId, string token, DateTime expiration)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));
			if (expiration == default(DateTime)) throw new ArgumentException("Not set", nameof(expiration));
			if (expiration.Kind != DateTimeKind.Utc) throw new ArgumentException("Is not UTC", nameof(expiration));

			UserId = userId;
			Token = token;
			Expiration = expiration;
		}
	}
}
