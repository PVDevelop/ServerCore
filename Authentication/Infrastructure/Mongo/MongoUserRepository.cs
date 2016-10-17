using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Authentication.Domain.Model;
using PVDevelop.UCoach.Mongo;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Mongo
{
	public class MongoUserRepository : IUserRepository, IValidator, IInitializer
	{
		private readonly IMongoRepository<MongoUser> _repository;
		private readonly IConnectionStringProvider _connectionStringProvider;

		public MongoUserRepository(
			IMongoRepository<MongoUser> repository,
			IConnectionStringProvider connectionStringProvider)
		{
			if (repository == null) throw new ArgumentNullException(nameof(repository));
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));

			_repository = repository;
			_connectionStringProvider = connectionStringProvider;
		}

		#region IValidator

		public void Validate()
		{
			MongoCollectionVersionHelper.ValidateByClassAttribute<MongoUser>(_connectionStringProvider);
		}

		#endregion

		#region IInitializer

		public void Initialize()
		{
			InitializeInidices();
			IniitializeCollectionVersion();
		}

		private void InitializeInidices()
		{
			//_logger.Debug(
			//    "Инициализирую коллекцию пользователей. Параметры подключения: {0}.",
			//    MongoHelper.SettingsToString(_connectionStirngProvider));

			var collection = MongoHelper.GetCollection<MongoUser>(_connectionStringProvider);

			var index = Builders<MongoUser>.IndexKeys.Ascending(u => u.Email);
			var options = new CreateIndexOptions()
			{
				Name = MongoHelper.GetIndexName<MongoUser>(nameof(MongoUser.Email)),
				Unique = true
			};

			collection.Indexes.CreateOne(index, options);

			//_logger.Debug("Инициализация коллекции пользователей прошла успешно.");
		}

		private void IniitializeCollectionVersion()
		{
			//_logger.Debug(
			//    "Инициализирую метаданные пользователей. Параметры подключения: {0}.",
			//    MongoHelper.SettingsToString(_connectionStirngProvider));

			MongoCollectionVersionHelper.InitializeCollectionVersion<MongoUser>(_connectionStringProvider);

			//_logger.Debug("Инициализация метаданных пользователей прошла успешно.");
		}

		#endregion

		#region IUserRepository

		public void Insert(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			var mongoUser = MapToMongoUser(user);
			_repository.Insert(mongoUser);
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

		#endregion
	}
}
