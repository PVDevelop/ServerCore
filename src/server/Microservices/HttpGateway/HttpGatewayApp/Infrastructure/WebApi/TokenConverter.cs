using System;
using System.Text;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public static class TokenConverter
	{
		public static string ConvertToString(TokenDto tokenDto)
		{
			var tokenStringBuilder = new StringBuilder();
			tokenStringBuilder.AppendLine(tokenDto.Token);
			tokenStringBuilder.AppendLine(tokenDto.Expiration.ToString("yyyy/MM/dd HH:mm:ss"));

			var tokenBytes = Encoding.UTF8.GetBytes(tokenStringBuilder.ToString());
			var tokenBase64Str = Convert.ToBase64String(tokenBytes);

			return tokenBase64Str;
		}
	}
}
