using System;
using MongoDB.Driver;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo
{
	public sealed class MongoConfirmationRepository :
		IConfirmationRepository,
		IValidator,
		IInitializer
	{
		private readonly IMongoRepository<MongoConfirmation> _repository;
		private readonly IConnectionStringProvider _connectionStringProvider;
		private readonly ILogger _logger = LoggerHelper.GetLogger<MongoConfirmationRepository>();

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
			MongoCollectionVersionHelper.ValidateByClassAttribute<MongoConfirmation>(
				_connectionStringProvider,
				_logger);
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
			_logger.Debug(
				$"Инициализирую коллекцию ключей подтверждения. Параметры подключения: {MongoHelper.SettingsToString(_connectionStringProvider)}.");

			var collection = MongoHelper.GetCollection<MongoConfirmation>(_connectionStringProvider);
			EnsureUserIdIndex(collection);
			EnsureKeyIndex(collection);

			_logger.Debug("Инициализация коллекции ключей подтверждения прошла успешно.");
		}

		private static void EnsureUserIdIndex(IMongoCollection<MongoConfirmation> collection)
		{
			var userIdIndex = Builders<MongoConfirmation>.IndexKeys.Ascending(u => u.UserId);
			var userIdIndexOptions = new CreateIndexOptions
			{
				Name = MongoHelper.GetIndexName<MongoConfirmation>(nameof(MongoConfirmation.UserId)),
				Unique = true
			};
			collection.Indexes.CreateOne(userIdIndex, userIdIndexOptions);
		}

		private static void EnsureKeyIndex(IMongoCollection<MongoConfirmation> collection)
		{
			var keyIndex = Builders<MongoConfirmation>.IndexKeys.Ascending(u => u.Key);
			var keyIndexOptions = new CreateIndexOptions
			{
				Name = MongoHelper.GetIndexName<MongoConfirmation>(nameof(MongoConfirmation.Key)),
				Unique = true
			};
			collection.Indexes.CreateOne(keyIndex, keyIndexOptions);
		}

		private void InitializeCollectionVersion()
		{
			_logger.Debug(
				$"Инициализирую метаданные пользователей. Параметры подключения: {MongoHelper.SettingsToString(_connectionStringProvider)}");

			MongoCollectionVersionHelper.InitializeCollectionVersion<MongoConfirmation>(_connectionStringProvider, _logger);

			_logger.Debug("Инициализация метаданных пользователей прошла успешно.");
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
