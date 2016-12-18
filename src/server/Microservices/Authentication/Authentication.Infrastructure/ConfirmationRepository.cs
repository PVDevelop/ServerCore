using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.EventStore;

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
			var confirmation = _eventSourcedAggregateRepository.RestoreEventSourcing<ConfirmationKey, IDomainEvent, Confirmation>(
				confirmationKey,
				(id, version, events) => new Confirmation(id, version, events));

			if (confirmation == null)
			{
				throw new InvalidOperationException($"Confirmation {confirmationKey} not found.");
			}

			return confirmation;
		}
	}
}
