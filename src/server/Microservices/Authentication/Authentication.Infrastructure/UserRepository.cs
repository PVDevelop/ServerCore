using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;

namespace PVDevelop.UCoach.Authentication.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		private readonly IEventSourcedAggregateRepository _eventSourcedAggregateRepository;

		public UserRepository(IEventSourcedAggregateRepository eventSourcedAggregateRepository)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));
			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
		}

		public void AddUser(User user)
		{
			_eventSourcedAggregateRepository.SaveAggregate(user);
		}
	}
}
