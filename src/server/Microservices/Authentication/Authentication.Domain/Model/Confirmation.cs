using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Events;
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

		public Confirmation(SagaId sagaId, ConfirmationKey confirmationKey, UserId userId) 
			: base(confirmationKey)
		{
			var confirmationCreated = new ConfirmationCreated(sagaId, confirmationKey, userId);

			Mutate(confirmationCreated);
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
			var @event = new ConfirmationTransmittedToPending(
				sagaId,
				Id);
			Mutate(@event);
		}

		public void Confirm(SagaId sagaId)
		{
			var @event = new ConfirmationApproved(
				sagaId, 
				UserId);
			Mutate(@event);
		}

		private void When(ConfirmationCreated @event)
		{
			State = ConfirmationState.New;
			UserId = @event.UserId;
		}

		private void When(ConfirmationTransmittedToPending @event)
		{
			State = ConfirmationState.Pending;
		}

		private void When(ConfirmationApproved @event)
		{
			State = ConfirmationState.Confirmed;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
