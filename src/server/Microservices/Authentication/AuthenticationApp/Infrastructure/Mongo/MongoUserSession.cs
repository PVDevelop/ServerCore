using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo
{
	public class MongoUserSession
	{
		public string Id { get; set; }

		public DateTime Expiration { get; set; }

		public MongoUserSession() { }

		public MongoUserSession(string id, DateTime expiration)
		{
			Id = id;
			Expiration = expiration;
		}
	}
}
