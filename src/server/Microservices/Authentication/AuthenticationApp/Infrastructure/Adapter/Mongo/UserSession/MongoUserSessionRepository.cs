using System;
using System.Linq;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.UserSession
{
	public class MongoUserSessionRepository : IUserSessionRepository
	{
		private readonly IMongoRepository<MongoUserSession> _repository;

		public MongoUserSessionRepository(IMongoRepository<MongoUserSession> repository)
		{
			_repository = repository;
		}

		public Domain.Model.UserSession[] GetByUserId(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));

			return 
				_repository.
				FindMany(us => us.UserId == userId).
				Select(MapToDomainUserSession).
				ToArray();
		}

		public void Insert(Domain.Model.UserSession userSession)
		{
			if (userSession == null) throw new ArgumentNullException(nameof(userSession));

			var mongoUserSession = MapToMongoUserSession(userSession);
			_repository.Insert(mongoUserSession);
		}

		private static Domain.Model.UserSession MapToDomainUserSession(MongoUserSession userSession)
		{
			return new Domain.Model.UserSession(userSession.Id, userSession.UserId, userSession.Expiration);
		}

		private static MongoUserSession MapToMongoUserSession(Domain.Model.UserSession userSession)
		{
			return new MongoUserSession
			{
				Id = userSession.Id,
				UserId = userSession.UserId,
				Expiration = userSession.Expiration
			};
		}
	}
}
