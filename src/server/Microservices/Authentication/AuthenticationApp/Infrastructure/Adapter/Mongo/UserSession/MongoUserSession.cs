using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.UserSession
{
	[MongoCollection("UserSessions")]
	public class MongoUserSession
	{
		public string Id { get; set; }

		[MongoIndexName("userId")]
		public string UserId { get; set; }

		public SessionState State { get; set; }
	}
}
