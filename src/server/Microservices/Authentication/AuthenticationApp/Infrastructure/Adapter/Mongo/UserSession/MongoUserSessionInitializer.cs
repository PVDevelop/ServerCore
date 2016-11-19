using System;
using MongoDB.Driver;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.Confirmation;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.UserSession
{
	public class MongoUserSessionInitializer : IInitializer
	{
		private readonly IConnectionStringProvider _connectionStringProvider;
		private readonly ILogger _logger = LoggerHelper.GetLogger<MongoConfirmationInitializer>();

		public MongoUserSessionInitializer(IConnectionStringProvider connectionStringProvider)
		{
			if (connectionStringProvider == null) throw new ArgumentNullException(nameof(connectionStringProvider));
			_connectionStringProvider = connectionStringProvider;
		}

		public void Initialize()
		{
			_logger.Debug($"Инициализирую коллекцию сессий пользователей. Параметры подключения: {MongoHelper.SettingsToString(_connectionStringProvider)}.");

			var collection = MongoHelper.GetCollection<MongoUserSession>(_connectionStringProvider);

			var index = Builders<MongoUserSession>.IndexKeys.Ascending(u => u.UserId);
			var options = new CreateIndexOptions()
			{
				Name = MongoHelper.GetIndexName<MongoUserSession>(nameof(MongoUserSession.UserId)),
				Unique = false
			};

			collection.Indexes.CreateOne(index, options);

			_logger.Debug("Инициализация коллекции сессий пользователей прошла успешно.");
		}
	}
}
