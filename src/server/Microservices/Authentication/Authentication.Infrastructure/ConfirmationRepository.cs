using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure
{
	public class ConfirmationRepository : IConfirmationRepository
	{
		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;

		public ConfirmationRepository(IEventSourcingRepository eventSourcedAggregateRepository)
		{
			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));
		}

		public void SaveConfirmation(Confirmation confirmation)
		{
			_eventSourcedAggregateRepository.SaveEventSourcing(confirmation);
		}

		public Confirmation GetConfirmation(ConfirmationKey confirmationKey)
		{
			return _eventSourcedAggregateRepository.RestoreEventSourcing<ConfirmationKey, IDomainEvent, Confirmation>(
				confirmationKey,
				(id, version, events) => new Confirmation(id, version, events));
		}
	}
}
