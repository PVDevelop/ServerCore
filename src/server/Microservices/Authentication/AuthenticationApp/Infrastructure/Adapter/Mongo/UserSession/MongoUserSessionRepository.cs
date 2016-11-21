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

		public Domain.Model.UserSession GetLastSession(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));

			var lastSession =
				_repository.
				FindMany(us => us.UserId == userId).
				SingleOrDefault();

			return lastSession == null ? null : MapToDomainUserSession(lastSession);
		}

		public void Insert(Domain.Model.UserSession userSession)
		{
			if (userSession == null) throw new ArgumentNullException(nameof(userSession));

			var mongoUserSession = MapToMongoUserSession(userSession);
			_repository.Insert(mongoUserSession);
		}

		public void Update(Domain.Model.UserSession userSession)
		{
			if (userSession == null) throw new ArgumentNullException(nameof(userSession));

			var mongoUserSession = MapToMongoUserSession(userSession);
			_repository.ReplaceOne(s => s.Id == mongoUserSession.Id, mongoUserSession);
		}

		private static Domain.Model.UserSession MapToDomainUserSession(MongoUserSession userSession)
		{
			return new Domain.Model.UserSession(userSession.Id, userSession.UserId, userSession.State);
		}

		private static MongoUserSession MapToMongoUserSession(Domain.Model.UserSession userSession)
		{
			return new MongoUserSession
			{
				Id = userSession.Id,
				UserId = userSession.UserId,
				State = userSession.State
			};
		}
	}
}
