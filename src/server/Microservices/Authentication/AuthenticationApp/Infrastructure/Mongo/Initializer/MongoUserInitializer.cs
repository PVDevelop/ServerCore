using System;
using MongoDB.Driver;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo.Initializer
{
	public class MongoUserInitializer : IInitializer
	{
		private readonly IConnectionStringProvider _connectionStringProvider;
		private readonly ILogger _logger = LoggerHelper.GetLogger<MongoUserInitializer>();

		public MongoUserInitializer(IConnectionStringProvider connectionStringProvider)
		{
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));
			_connectionStringProvider = connectionStringProvider;
		}

		public void Initialize()
		{
			_logger.Debug($"Инициализирую коллекцию пользователей. Параметры подключения: {MongoHelper.SettingsToString(_connectionStringProvider)}.");

			var collection = MongoHelper.GetCollection<MongoUser>(_connectionStringProvider);

			var index = Builders<MongoUser>.IndexKeys.Ascending(u => u.Email);
			var options = new CreateIndexOptions()
			{
				Name = MongoHelper.GetIndexName<MongoUser>(nameof(MongoUser.Email)),
				Unique = true
			};

			collection.Indexes.CreateOne(index, options);

			_logger.Debug("Инициализация коллекции пользователей прошла успешно.");
		}
	}
}
