using System;
using PVDevelop.UCoach.Domain;
using PVDevelop.UCoach.Domain.Model.Confirmation;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class ConfirmationRepository : IConfirmationRepository
	{
		private readonly IEventSourcingRepository _eventSourcedAggregateRepository;

		public ConfirmationRepository(IEventSourcingRepository eventSourcedAggregateRepository)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
		}

		public void SaveConfirmation(ConfirmationAggregate confirmation)
		{
			_eventSourcedAggregateRepository.SaveEventSourcing<
				ConfirmationHelper,
				ConfirmationKey,
				IDomainEvent,
				ConfirmationAggregate>(confirmation);
		}

		public ConfirmationAggregate GetConfirmation(ConfirmationKey confirmationKey)
		{
			return _eventSourcedAggregateRepository.RestoreEventSourcing<
				ConfirmationHelper,
				ConfirmationKey,
				IDomainEvent,
				ConfirmationAggregate>(confirmationKey);
		}
	}
}
