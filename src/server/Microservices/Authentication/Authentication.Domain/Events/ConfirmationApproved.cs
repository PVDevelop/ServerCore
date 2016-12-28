using System;
using PVDevelop.UCoach.Domain.Model.Confirmation;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationApproved : 
		AProcessEvent, 
		IDomainEvent
	{
		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationApproved(
			ProcessId processId,
			ConfirmationKey confirmationKey,
			UserId userId) :
			base(processId, UserConfirmationProcessState.ConfirmationApproved)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			ConfirmationKey = confirmationKey;
			UserId = userId;
		}
	}
}