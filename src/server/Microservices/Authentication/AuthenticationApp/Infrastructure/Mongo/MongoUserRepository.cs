﻿using System;
using MongoDB.Driver;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo
{
	public class MongoUserRepository : IUserRepository, IValidator, IInitializer
	{
		private readonly IMongoRepository<MongoUser> _repository;
		private readonly IConnectionStringProvider _connectionStringProvider;
		private readonly ILogger _logger = LoggerHelper.GetLogger<MongoUserRepository>();

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
			MongoCollectionVersionHelper.ValidateByClassAttribute<MongoUser>(_connectionStringProvider, _logger);
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

		private void IniitializeCollectionVersion()
		{
			_logger.Debug($"Инициализирую метаданные пользователей. Параметры подключения: {MongoHelper.SettingsToString(_connectionStringProvider)}.");

			MongoCollectionVersionHelper.InitializeCollectionVersion<MongoUser>(_connectionStringProvider, _logger);

			_logger.Debug("Инициализация метаданных пользователей прошла успешно.");
		}

		#endregion

		#region IUserRepository

		public User GetById(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Not set", nameof(userId));

			var mongoUser = _repository.Find(u => u.Id == userId);
			return mongoUser == null ? null : MapToDomainUser(mongoUser);
		}

		public void Insert(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException(nameof(user));
			}

			var mongoUser = MapToMongoUser(user);
			_repository.Insert(mongoUser);
		}

		public void Update(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			var mongoUser = MapToMongoUser(user);
			_repository.ReplaceOne(u => u.Id == mongoUser.Id, mongoUser);
		}

		private static MongoUser MapToMongoUser(User user)
		{
			var mongoUserSession = 
				user.Session == null ? 
				null : 
				new MongoUserSession(user.Session.Id, user.Session.Expiration);

			return new MongoUser
			{
				Id = user.Id,
				Email = user.Email,
				Password = user.Password,
				CreationTime = user.CreationTime,
				State = user.State,
				Session = mongoUserSession
			};
		}

		private static User MapToDomainUser(MongoUser user)
		{
			var session = 
				user.Session == null ? 
				null : 
				new UserSession(user.Session.Id, user.Session.Expiration);

			return new User(
				id: user.Id,
				email: user.Email,
				password: user.Password,
				state: user.State,
				creationTime: user.CreationTime,
				session: session);
		}

		#endregion
	}
}
