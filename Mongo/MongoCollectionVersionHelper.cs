using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.Mongo
{
	public static class MongoCollectionVersionHelper
	{
		public static void ValidateByClassAttribute<TCollection>(IConnectionStringProvider connectionStringProvider)
		{
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));

			var referencedCollectionName = MongoHelper.GetCollectionName<TCollection>();

			//_logger.Debug("Проверяю метаданные коллекции {0}.", referencedCollectionName);

			var collection = MongoHelper.GetCollection<CollectionVersion>(connectionStringProvider);
			var collectionVersion = collection.Find(cv => cv.TargetCollectionName == referencedCollectionName).SingleOrDefault();

			var requiredVersion = MongoHelper.GetDataVersion<TCollection>();
			if (collectionVersion == null)
			{
				//_logger.Error("Метаданные коллекции {0} не заданы.", referencedCollectionName);
				throw new MongoCollectionNotInitializedException(0, requiredVersion);
			}
			if (collectionVersion.TargetVersion != requiredVersion)
			{
				//_logger.Error(
					//"Версия коллекции {0} неверна. Ожидалась - {1}, текущая - {2}.",
					//referencedCollectionName,
					//requiredVersion,
					//collectionVersion.TargetVersion);
				throw new MongoCollectionNotInitializedException(collectionVersion.TargetVersion, requiredVersion);
			}
		}

		public static void InitializeCollectionVersion<TCollection>(IConnectionStringProvider connectionStringProvider)
		{
			//_logger.Debug(
			//    "Инициализирую метаданные пользователей. Параметры подключения: {0}.",
			//    MongoHelper.SettingsToString(_connectionStirngProvider));

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

			//_logger.Debug("Инициализация метаданных пользователей прошла успешно.");
		}
	}
}
