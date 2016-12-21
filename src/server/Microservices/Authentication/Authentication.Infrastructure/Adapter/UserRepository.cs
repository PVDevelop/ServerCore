using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class UserRepository : IUserRepository
	{
		public const string StreamIdPrefix = "Aggregate.User";

		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;

		public UserRepository(IEventSourcingRepository eventSourcedAggregateRepository)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
		}

		public void SaveUser(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			_eventSourcedAggregateRepository.SaveEventSourcing(StreamIdPrefix, user);
		}

		public User GetUserById(UserId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return _eventSourcedAggregateRepository.RestoreEventSourcing<UserId, IDomainEvent, User>(
				StreamIdPrefix,
				id,
				(userId, version, events) => new User(userId, version, events));
		}
	}
}
