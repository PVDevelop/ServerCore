using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationCreated :
		AProcessEvent,
		IDomainEvent
	{
		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationCreated(
			ProcessId processId,
			ConfirmationKey confirmationKey,
			UserId userId) :
			base(processId, UserRegistrationProcessState.ConfirmationCreated)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
			ConfirmationKey = confirmationKey;
		}
	}
}
