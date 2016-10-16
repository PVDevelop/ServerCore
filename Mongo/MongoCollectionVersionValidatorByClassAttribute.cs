using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.Mongo
{
	public class MongoCollectionVersionValidatorByClassAttribute :
		IMongoCollectionVersionValidator
	{
		private readonly IConnectionStringProvider _settings;
		//private readonly ILogger _logger = LoggerFactory.CreateLogger<MongoCollectionVersionValidatorByClassAttribute>();

		public MongoCollectionVersionValidatorByClassAttribute(IConnectionStringProvider settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException(nameof(settings));
			}
			_settings = settings;
		}

		public void Validate<T>()
		{
			var referencedCollectionName = MongoHelper.GetCollectionName<T>();

			//_logger.Debug("Проверяю метаданные коллекции {0}.", referencedCollectionName);

			var collection = MongoHelper.GetCollection<CollectionVersion>(_settings);
			var collectionVersion = collection.Find(cv => cv.TargetCollectionName == referencedCollectionName).SingleOrDefault();

			var requiredVersion = MongoHelper.GetDataVersion<T>();
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
	}
}
