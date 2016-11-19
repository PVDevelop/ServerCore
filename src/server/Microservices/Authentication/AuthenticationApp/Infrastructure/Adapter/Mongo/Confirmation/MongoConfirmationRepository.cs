using System;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;
using PVDevelop.UCoach.Mongo;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.Mongo.Confirmation
{
	public sealed class MongoConfirmationRepository :
		IConfirmationRepository
	{
		private readonly IMongoRepository<MongoConfirmation> _repository;

		public MongoConfirmationRepository(
			IMongoRepository<MongoConfirmation> repository)
		{
			if (repository == null) throw new ArgumentNullException(nameof(repository));
			_repository = repository;
		}

		public void Insert(Domain.Model.Confirmation confirmation)
		{
			if (confirmation == null) throw new ArgumentNullException(nameof(confirmation));

			var mongoConfirmation = MapToMongoConfirmation(confirmation);
			_repository.Insert(mongoConfirmation);
		}

		public Domain.Model.Confirmation FindByConfirmationKey(string key)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set", nameof(key));

			var mongoConfirmation = _repository.Find(c => c.Key == key);
			return mongoConfirmation == null ? null : MapToDomainConfirmation(mongoConfirmation);
		}

		public void Update(Domain.Model.Confirmation confirmation)
		{
			if (confirmation == null) throw new ArgumentNullException(nameof(confirmation));

			var mongoConfirmation = MapToMongoConfirmation(confirmation);
			_repository.ReplaceOne(c => c.Key == mongoConfirmation.Key, mongoConfirmation);
		}

		private static MongoConfirmation MapToMongoConfirmation(Domain.Model.Confirmation confirmation)
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

		private static Domain.Model.Confirmation MapToDomainConfirmation(MongoConfirmation confirmation)
		{
			return new Domain.Model.Confirmation(
				confirmation.Id,
				confirmation.UserId,
				confirmation.Key,
				confirmation.State,
				confirmation.CreationTime);
		}
	}
}
