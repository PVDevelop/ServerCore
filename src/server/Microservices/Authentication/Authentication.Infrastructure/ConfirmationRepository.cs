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
		private readonly string _confirmationStreamIdPrefix;

		public ConfirmationRepository(
			IEventSourcingRepository eventSourcedAggregateRepository,
			string confirmationStreamIdPrefix)
		{
			if (eventSourcedAggregateRepository == null)
				throw new ArgumentNullException(nameof(eventSourcedAggregateRepository));
			if(string.IsNullOrWhiteSpace(confirmationStreamIdPrefix))
				throw new ArgumentException("Not set", nameof(confirmationStreamIdPrefix));

			_eventSourcedAggregateRepository = eventSourcedAggregateRepository;
			_confirmationStreamIdPrefix = confirmationStreamIdPrefix;
		}

		public void SaveConfirmation(Confirmation confirmation)
		{
			_eventSourcedAggregateRepository.SaveEventSourcing(_confirmationStreamIdPrefix, confirmation);
		}

		public Confirmation GetConfirmation(ConfirmationKey confirmationKey)
		{
			return _eventSourcedAggregateRepository.RestoreEventSourcing<ConfirmationKey, IDomainEvent, Confirmation>(
				_confirmationStreamIdPrefix,
				confirmationKey,
				(id, version, events) => new Confirmation(id, version, events));
		}
	}
}
