using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Model.Confirmation
{
	/// <summary>
	/// Доменная модель - подтверждение
	/// </summary>
	public sealed class ConfirmationAggregate : AEventSourcedAggregate<ConfirmationKey>
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public UserId UserId { get; private set; }

		/// <summary>
		/// Состояние подтверждения.
		/// </summary>
		public ConfirmationState State { get; private set; }

		public ConfirmationAggregate(ProcessId processId, ConfirmationKey confirmationKey, UserId userId) 
			: base(confirmationKey)
		{
			var confirmationCreated = new ConfirmationCreated(processId, confirmationKey, userId);

			Mutate(confirmationCreated);
		}

		public ConfirmationAggregate(ConfirmationKey confirmationKey, int initialVersion, IEnumerable<IDomainEvent> domainEvents)
			: base(confirmationKey, initialVersion, domainEvents)
		{
		}

		public void Confirm(ProcessId processId)
		{
			if (processId == null) throw new ArgumentNullException(nameof(processId));

			Mutate(new ConfirmationApproved(processId, Id, UserId));
		}

		protected override void When(IDomainEvent @event)
		{
			When((dynamic)@event);
		}

		private void When(ConfirmationCreated @event)
		{
			State = ConfirmationState.Pending;
			UserId = @event.UserId;
		}

		private void When(ConfirmationApproved @event)
		{
			State = ConfirmationState.Approved;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
