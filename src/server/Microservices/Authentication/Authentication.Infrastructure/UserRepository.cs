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
		private readonly string _userStreamIdPrefix;

		public UserRepository(
			IEventSourcingRepository eventSourcedAggregateRepository,
			string userStreamIdPrefix)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));
			if(string.IsNullOrWhiteSpace(userStreamIdPrefix))
				throw new ArgumentException("Not set", nameof(_userStreamIdPrefix));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
			_userStreamIdPrefix = userStreamIdPrefix;
		}

		public void SaveUser(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			_eventSourcedAggregateRepository.SaveEventSourcing(_userStreamIdPrefix, user);
		}

		public User GetUserById(UserId id)
		{
			if (id == null) throw new ArgumentNullException(nameof(id));
			return _eventSourcedAggregateRepository.RestoreEventSourcing<UserId, IDomainEvent, User>(
				_userStreamIdPrefix,
				id,
				(userId, version, events) => new User(userId, version, events));
		}
	}
}
