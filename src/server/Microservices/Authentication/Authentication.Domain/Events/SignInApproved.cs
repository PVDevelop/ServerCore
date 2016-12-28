using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class SignInApproved : 
		AProcessEvent,
		IDomainEvent
	{
		public UserId UserId { get; }

		public SignInApproved(ProcessId processId, UserId userId) 
			: base(processId, new UserSignInProcessState(UserSignInMachineState.SignInApproved))
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
