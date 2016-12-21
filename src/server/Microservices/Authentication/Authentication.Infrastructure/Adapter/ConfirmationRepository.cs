using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class ConfirmationRepository : IConfirmationRepository
	{
		public const string StreamIdPrefix = "Aggregate.Confirmation";

		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;

		public ConfirmationRepository(IEventSourcingRepository eventSourcedAggregateRepository)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
		}

		public void SaveConfirmation(Confirmation confirmation)
		{
			_eventSourcedAggregateRepository.SaveEventSourcing(StreamIdPrefix, confirmation);
		}

		public Confirmation GetConfirmation(ConfirmationKey confirmationKey)
		{
			return _eventSourcedAggregateRepository.RestoreEventSourcing<ConfirmationKey, IDomainEvent, Confirmation>(
				StreamIdPrefix,
				confirmationKey,
				(id, version, events) => new Confirmation(id, version, events));
		}
	}
}
