using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo
{
	public class MongoUserToken
	{
		public string Token { get; set; }

		public DateTime Expiration { get; set; }

		public MongoUserToken() { }

		public MongoUserToken(string token, DateTime expiration)
		{
			Token = token;
			Expiration = expiration;
		}
	}
}
