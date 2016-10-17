using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Authentication.Domain.Model;
using PVDevelop.UCoach.Mongo;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Mongo
{
	public sealed class MongoConfirmationRepository :
		IConfirmationRepository,
		IValidator,
		IInitializer
	{
		private readonly IMongoRepository<MongoConfirmation> _repository;
		private readonly IConnectionStringProvider _connectionStringProvider;

		public MongoConfirmationRepository(
			IMongoRepository<MongoConfirmation> repository,
			IConnectionStringProvider connectionStringProvider)
		{
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));

			_repository = repository;
			_connectionStringProvider = connectionStringProvider;
		}

		#region IValidator

		public void Validate()
		{
			MongoCollectionVersionHelper.ValidateByClassAttribute<MongoConfirmation>(_connectionStringProvider);
		}

		#endregion

		#region IInitializer

		public void Initialize()
		{
			InitializeIndices();
			InitializeCollectionVersion();
		}

		private void InitializeIndices()
		{
			//_logger.Debug(
			//	"Инициализирую коллекцию ключей подтверждения. Параметры подключения: {0}.",
			//	MongoHelper.SettingsToString(_settings));

			var collection = MongoHelper.GetCollection<MongoConfirmation>(_connectionStringProvider);

			var index = Builders<MongoConfirmation>.IndexKeys.Ascending(u => u.Key);
			var options = new CreateIndexOptions()
			{
				Name = MongoHelper.GetIndexName<MongoConfirmation>(nameof(MongoConfirmation.UserId)),
				Unique = true
			};

			collection.Indexes.CreateOne(index, options);

			//_logger.Debug("Инициализация коллекции ключей подтверждения прошла успешно.");
		}

		private void InitializeCollectionVersion()
		{
			//_logger.Debug(
			//    "Инициализирую метаданные пользователей. Параметры подключения: {0}.",
			//    MongoHelper.SettingsToString(_connectionStirngProvider));

			MongoCollectionVersionHelper.InitializeCollectionVersion<MongoConfirmation>(_connectionStringProvider);

			//_logger.Debug("Инициализация метаданных пользователей прошла успешно.");
		}

		#endregion

		#region IConfirmationRepository

		public void Insert(Confirmation confirmation)
		{
			if (confirmation == null) throw new ArgumentNullException(nameof(confirmation));

			var mongoConfirmation = MapToMongoConfirmation(confirmation);
			_repository.Insert(mongoConfirmation);
		}

		private static MongoConfirmation MapToMongoConfirmation(Confirmation confirmation)
		{
			return new MongoConfirmation
			{
				CreationTime = confirmation.CreationTime,
				Key = confirmation.Key,
				UserId = confirmation.UserId
			};
		}

		#endregion
	}
}
