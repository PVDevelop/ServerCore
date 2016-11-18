using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo.Initializer
{
	public class MongoConfirmationInitializer : IInitializer
	{
		private readonly IConnectionStringProvider _connectionStringProvider;
		private readonly ILogger _logger = LoggerHelper.GetLogger<MongoConfirmationInitializer>();

		public MongoConfirmationInitializer(IConnectionStringProvider connectionStringProvider)
		{
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));
			_connectionStringProvider = connectionStringProvider;
		}

		public void Initialize()
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
	}
}
