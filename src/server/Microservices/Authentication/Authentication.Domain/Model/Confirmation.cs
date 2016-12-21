using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

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

		public void TransmitToPending(SagaId sagaId)
		{
			var @event = new ConfirmationTransmittedToPendingEvent(
				sagaId,
				new UserCreationProgress(UserCreationStatus.Pending), 
				Id);
			Mutate(@event);
		}

		public void Confirm(SagaId sagaId)
		{
			var @event = new ConfirmationApprovedEvent(
				sagaId, 
				new UserCreationProgress(UserCreationStatus.Created), 
				UserId);
			Mutate(@event);
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

		private void When(ConfirmationApprovedEvent @event)
		{
			State = ConfirmationState.Confirmed;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
