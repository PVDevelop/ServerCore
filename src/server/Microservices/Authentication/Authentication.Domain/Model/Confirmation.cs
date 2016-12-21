using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Events;

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

		public Confirmation(ConfirmationKey confirmationKey, UserId userId) 
			: base(confirmationKey)
		{
			var confirmationCreated = new ConfirmationCreated(confirmationKey, userId);

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

		public void TransmitToPending()
		{
			var @event = new ConfirmationTransmittedToPending(Id, UserId);
			Mutate(@event);
		}

		public void Confirm()
		{
			var @event = new ConfirmationApproved(UserId);
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
