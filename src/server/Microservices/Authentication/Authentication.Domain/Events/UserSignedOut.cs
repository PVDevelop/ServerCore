using System;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserSignedOut :
		AProcessEvent,
		IDomainEvent
	{
		public UserId UserId { get; }

		public UserSignedOut(ProcessId processId, UserId userId)
			: base(processId, UserSignOutProcessState.SignedOut)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
