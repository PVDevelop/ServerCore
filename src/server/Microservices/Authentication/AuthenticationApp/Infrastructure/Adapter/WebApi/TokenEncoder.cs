using System;
using System.Text;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi
{
	public static class TokenEncoder
	{
		public static string Encode(AccessToken tokenDto)
		{
			if (tokenDto == null) throw new ArgumentNullException(nameof(tokenDto));

			var tokenStringBuilder = new StringBuilder();

			tokenStringBuilder.AppendLine(tokenDto.UserId);
			tokenStringBuilder.AppendLine(tokenDto.Token);

			var expirationTicks = tokenDto.Expiration.Ticks;
			tokenStringBuilder.AppendLine(expirationTicks.ToString());

			var tokenBytes = Encoding.UTF8.GetBytes(tokenStringBuilder.ToString());
			var tokenBase64Str = Convert.ToBase64String(tokenBytes);

			return tokenBase64Str;
		}

		public static AccessToken Decode(string token)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));

			var tokenBytes = Convert.FromBase64String(token);
			var decodedToken = Encoding.UTF8.GetString(tokenBytes);

			var splittedStrings = decodedToken.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			var expirationTicks = long.Parse(splittedStrings[2]);
			var expiration = new DateTime(expirationTicks, DateTimeKind.Utc);

			var accessToken = new AccessToken(
				userId: splittedStrings[0],
				token: splittedStrings[1],
				expiration: expiration);

			return accessToken;
		}
	}
}
