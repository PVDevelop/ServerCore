using System;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserSignOutRequested : 
		AProcessEvent
	{
		public UserId UserId { get; }

		public UserSignOutRequested(
			ProcessId processId,
			UserId userId) 
			: base(processId, UserSignOutProcessState.SignOutRequested)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
