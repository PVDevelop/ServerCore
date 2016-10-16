using System;
using PVDevelop.UCoach.Authentication.Domain.Model;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Mongo
{
	public class MongoUserRepository : IUserRepository
	{
		private readonly IMongoRepository<MongoUser> _repository;
		private readonly IMongoCollectionVersionValidator _versionCollectionValidator;

		public MongoUserRepository(
			IMongoRepository<MongoUser> repository,
			IMongoCollectionVersionValidator versionCollectionValidator)
		{
			if (repository == null)
			{
				throw new ArgumentNullException(nameof(repository));
			}
			if (versionCollectionValidator == null)
			{
				throw new ArgumentNullException(nameof(versionCollectionValidator));
			}

			_repository = repository;
			_versionCollectionValidator = versionCollectionValidator;
		}

		public void Insert(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			_versionCollectionValidator.Validate<MongoUser>();

			var mongoUser = MapToMongoUser(user);
			_repository.Insert(mongoUser);
		}

		public User FindByEmail(string email)
		{
			if (email == null)
			{
				throw new ArgumentNullException(nameof(email));
			}

			var mongoUser = _repository.Find(u => u.Email == email);
			return MapToDomainUser(mongoUser);
		}

		public User FindById(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException(nameof(id));
			}

			var mongoUser = _repository.Find(u => u.Id == id);
			return MapToDomainUser(mongoUser);
		}

		public void Update(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			_versionCollectionValidator.Validate<MongoUser>();

			var mongoUser = MapToMongoUser(user);
			_repository.ReplaceOne(u => u.Id == user.Id, mongoUser);
		}

		private static MongoUser MapToMongoUser(User user)
		{
			return new MongoUser()
			{
				Id = user.Id,
				Email = user.Email,
				Password = user.Password,
				CreationTime = user.CreationTime,
				Status = user.ConfirmationStatus
			};
		}

		private static User MapToDomainUser(MongoUser mongoUser)
		{
			return new User(
				mongoUser.Id,
				mongoUser.Email,
				mongoUser.Password,
				mongoUser.CreationTime,
				mongoUser.Status);
		}
	}
}
