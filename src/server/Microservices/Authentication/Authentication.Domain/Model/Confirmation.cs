using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Messages;

namespace PVDevelop.UCoach.Domain.Model
{
	/// <summary>
	/// Доменная модель - подтверждение
	/// </summary>
	public sealed class Confirmation : AEventSourcedAggregate<ConfirmationKey>
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public UserId UserId { get; private set; }

		/// <summary>
		/// Состояние подтверждения.
		/// </summary>
		public ConfirmationState State { get; private set; }

		public Confirmation(ConfirmationCreatedEvent confirmationCreatedEvent) 
			: base(confirmationCreatedEvent.ConfirmationKey)
		{
			Mutate(confirmationCreatedEvent);
		}

		public Confirmation(ConfirmationKey confirmationKey, int initialVersion, IEnumerable<IDomainEvent> domainEvents)
			: base(confirmationKey, initialVersion, domainEvents)
		{
		}

		protected override void When(IDomainEvent @event)
		{
			When((dynamic)@event);
		}

		private void When(ConfirmationCreatedEvent @event)
		{
			State = ConfirmationState.New;
			UserId = @event.UserId;
		}

		private void When(ConfirmationTransmittedToPendingEvent @event)
		{
			State = ConfirmationState.Pending;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
