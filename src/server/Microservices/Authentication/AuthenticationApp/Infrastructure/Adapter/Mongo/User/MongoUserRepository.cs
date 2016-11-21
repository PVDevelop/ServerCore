using System;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.User
{
	public class MongoUserRepository : IUserRepository
	{
		private readonly IMongoRepository<MongoUser> _repository;

		public MongoUserRepository(
			IMongoRepository<MongoUser> repository)
		{
			if (repository == null) throw new ArgumentNullException(nameof(repository));
			_repository = repository;
		}

		public Domain.Model.User GetById(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));

			var mongoUser = _repository.Find(u => u.Id == userId);
			return mongoUser == null ? null : MapToDomainUser(mongoUser);
		}

		public Domain.Model.User GetByEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));

			var mongoUser = _repository.Find(u => u.Email == email);
			return mongoUser == null ? null : MapToDomainUser(mongoUser);
		}

		public void Insert(Domain.Model.User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			var mongoUser = MapToMongoUser(user);
			_repository.Insert(mongoUser);
		}

		public void Update(Domain.Model.User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			var mongoUser = MapToMongoUser(user);
			_repository.ReplaceOne(u => u.Id == mongoUser.Id, mongoUser);
		}

		private static MongoUser MapToMongoUser(Domain.Model.User user)
		{
			return new MongoUser
			{
				Id = user.Id,
				Email = user.Email,
				Password = user.Password,
				CreationTime = user.CreationTime,
			};
		}

		private static Domain.Model.User MapToDomainUser(MongoUser user)
		{
			return new Domain.Model.User(
				id: user.Id,
				email: user.Email,
				password: user.Password,
				creationTime: user.CreationTime);
		}
	}
}
