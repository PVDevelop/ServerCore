using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
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

		public void AddUser(User user)
		{
			_eventSourcedAggregateRepository.SaveEventSourcing(user);
		}
	}
}
