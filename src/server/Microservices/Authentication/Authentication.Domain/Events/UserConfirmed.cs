using System;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserConfirmed : 
		AProcessEvent,
		IDomainEvent
	{
		public UserId UserId { get; }

		public UserConfirmed(
			ProcessId processId,
			UserId userId) 
			: base(processId, UserConfirmationProcessState.UserConfirmed)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
