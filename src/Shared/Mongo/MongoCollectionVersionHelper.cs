using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;

namespace PVDevelop.UCoach.Mongo
{
	public static class MongoCollectionVersionHelper
	{
		public static void ValidateByClassAttribute<TCollection>(
			IConnectionStringProvider connectionStringProvider,
			ILogger logger)
		{
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));

			var referencedCollectionName = MongoHelper.GetCollectionName<TCollection>();

			logger.Debug($"Проверяю метаданные коллекции {referencedCollectionName}.");

			var collection = MongoHelper.GetCollection<CollectionVersion>(connectionStringProvider);
			var collectionVersion = collection.Find(cv => cv.TargetCollectionName == referencedCollectionName).SingleOrDefault();

			var requiredVersion = MongoHelper.GetDataVersion<TCollection>();
			if (collectionVersion == null)
			{
				logger.Error($"Метаданные коллекции {referencedCollectionName} не заданы.");
				throw new MongoCollectionNotInitializedException(0, requiredVersion);
			}
			if (collectionVersion.TargetVersion != requiredVersion)
			{
				logger.Error($"Версия коллекции {referencedCollectionName} неверна. Ожидалась - {requiredVersion}, текущая - {collectionVersion.TargetVersion}.");
				throw new MongoCollectionNotInitializedException(collectionVersion.TargetVersion, requiredVersion);
			}
		}

		public static void InitializeCollectionVersion<TCollection>(
			IConnectionStringProvider connectionStringProvider,
			ILogger logger)
		{
			logger.Debug($"Инициализирую метаданные пользователей. Параметры подключения: {MongoHelper.SettingsToString(connectionStringProvider)}.");

			var collection = MongoHelper.GetCollection<CollectionVersion>(connectionStringProvider);
			var collectionVersion = new CollectionVersion(MongoHelper.GetCollectionName<TCollection>())
			{
				TargetVersion = MongoHelper.GetDataVersion<TCollection>()
			};

			var options = new UpdateOptions()
			{
				IsUpsert = true
			};

			if (collection.Find(cv => cv.TargetCollectionName == collectionVersion.TargetCollectionName).SingleOrDefault() == null)
			{
				collection.ReplaceOne(cv => cv.TargetCollectionName == collectionVersion.TargetCollectionName, collectionVersion, options);
			}

			logger.Debug("Инициализация метаданных пользователей прошла успешно.");
		}
	}
}
