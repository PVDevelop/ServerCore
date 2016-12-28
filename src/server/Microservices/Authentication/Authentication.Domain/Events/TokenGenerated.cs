using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class TokenGenerated :
		AProcessEvent,
		IDomainEvent
	{
		public UserAccessToken Token { get; }

		public TokenGenerated(ProcessId processId, UserAccessToken token)
			: base(processId, new UserSignInProcessState(token))
		{
			if (token == null) throw new ArgumentNullException(nameof(token));

			Token = token;
		}
	}
}
