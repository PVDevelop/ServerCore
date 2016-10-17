using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Mongo
{
	/// <summary>
	/// Создает коллекцию пользователей и навешивает нужные ключи, индексы
	/// </summary>
	public class MongoUserCollectionInitializer : IInitializer
	{
		private readonly IConnectionStringProvider _connectionStirngProvider;
		//private readonly ILogger _logger = LoggerFactory.CreateLogger<MongoUserCollectionInitializer>();

		public MongoUserCollectionInitializer(
			IConnectionStringProvider connectionStirngProvider)
		{
			if (connectionStirngProvider == null) throw new ArgumentNullException(nameof(connectionStirngProvider));

			_connectionStirngProvider = connectionStirngProvider;
		}

		public void Initialize()
		{
			InitUserCollection();
			InitVersionCollection();
		}

		private void InitUserCollection()
		{
			//_logger.Debug(
			//    "Инициализирую коллекцию пользователей. Параметры подключения: {0}.",
			//    MongoHelper.SettingsToString(_connectionStirngProvider));

			var collection = MongoHelper.GetCollection<MongoUser>(_connectionStirngProvider);

			var index = Builders<MongoUser>.IndexKeys.Ascending(u => u.Email);
			var options = new CreateIndexOptions()
			{
				Name = MongoHelper.GetIndexName<MongoUser>(nameof(MongoUser.Email)),
				Unique = true
			};

			collection.Indexes.CreateOne(index, options);

			//_logger.Debug("Инициализация коллекции пользователей прошла успешно.");
		}

		private void InitVersionCollection()
		{
			//_logger.Debug(
			//    "Инициализирую метаданные пользователей. Параметры подключения: {0}.",
			//    MongoHelper.SettingsToString(_connectionStirngProvider));

			var collection = MongoHelper.GetCollection<CollectionVersion>(_connectionStirngProvider);
			var collectionVersion = new CollectionVersion(MongoHelper.GetCollectionName<MongoUser>())
			{
				TargetVersion = MongoHelper.GetDataVersion<MongoUser>()
			};

			var options = new UpdateOptions()
			{
				IsUpsert = true
			};

			if (collection.Find(cv => cv.TargetCollectionName == collectionVersion.TargetCollectionName).SingleOrDefault() == null)
			{
				collection.ReplaceOne(cv => cv.TargetCollectionName == collectionVersion.TargetCollectionName, collectionVersion, options);
			}

			//_logger.Debug("Инициализация метаданных пользователей прошла успешно.");
		}
	}
}
