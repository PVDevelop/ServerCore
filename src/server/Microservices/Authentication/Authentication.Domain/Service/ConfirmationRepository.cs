using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Domain.Service
{
	public class ConfirmationRepository : IConfirmationRepository
	{
		private readonly IEventSourcedAggregateRepository _eventSourcedAggregateRepository;

		public ConfirmationRepository(IEventSourcedAggregateRepository eventSourcedAggregateRepository)
		{
			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));
		}

		public void SaveConfirmation(Confirmation confirmation)
		{
			_eventSourcedAggregateRepository.SaveAggregate(confirmation);
		}

		public Confirmation GetConfirmation(ConfirmationKey confirmationKey)
		{
			return _eventSourcedAggregateRepository.RestoreAggregate(
				confirmationKey,
				(id, version, events) => new Confirmation(id, version, events));
		}
	}
}
