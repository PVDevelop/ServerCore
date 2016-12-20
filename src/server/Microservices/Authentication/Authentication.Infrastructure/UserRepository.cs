using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure
{
	public class UserRepository : IUserRepository
	{
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
			_eventSourcedAggregateRepository.SaveEventSourcing(user);
		}

		public User GetUserById(UserId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return _eventSourcedAggregateRepository.RestoreEventSourcing<UserId, IDomainEvent, User>(
				id,
				(userId, version, events) => new User(userId, version, events));
		}
	}
}
