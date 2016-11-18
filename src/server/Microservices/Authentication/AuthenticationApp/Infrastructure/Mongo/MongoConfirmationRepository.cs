using System;
using MongoDB.Driver;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Logging;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Mongo
{
	public sealed class MongoConfirmationRepository :
		IConfirmationRepository
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

		public void Insert(Confirmation confirmation)
		{
			if (confirmation == null) throw new ArgumentNullException(nameof(confirmation));

			var mongoConfirmation = MapToMongoConfirmation(confirmation);
			_repository.Insert(mongoConfirmation);
		}

		public Confirmation FindByConfirmationKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set", nameof(key));

			var mongoConfirmation = _repository.Find(c => c.Key == key);
			return mongoConfirmation == null ? null : MapToDomainConfirmation(mongoConfirmation);
		}

		public void Update(Confirmation confirmation)
		{
			if (confirmation == null) throw new ArgumentNullException(nameof(confirmation));

			var mongoConfirmation = MapToMongoConfirmation(confirmation);
			_repository.ReplaceOne(c => c.Key == mongoConfirmation.Key, mongoConfirmation);
		}

		private static MongoConfirmation MapToMongoConfirmation(Confirmation confirmation)
		{
			return new MongoConfirmation
			{
				Id = confirmation.Id,
				Key = confirmation.Key,
				UserId = confirmation.UserId,
				State = confirmation.State,
				CreationTime = confirmation.CreationTime
			};
		}

		private static Confirmation MapToDomainConfirmation(MongoConfirmation confirmation)
		{
			return new Confirmation(
				confirmation.Id,
				confirmation.UserId,
				confirmation.Key,
				confirmation.State,
				confirmation.CreationTime);
		}
	}
}
