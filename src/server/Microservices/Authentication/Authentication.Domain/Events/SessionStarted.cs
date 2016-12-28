using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class SessionStarted : 
		AProcessEvent, 
		IDomainEvent
	{
		public UserSessionId SessionId { get; }
		public UserId UserId { get; }

		public SessionStarted(
			ProcessId processId,
			UserSessionId sessionId,
			UserId userId) 
			: base(processId, UserConfirmationProcessState.SessionStarted)
		{
			if (sessionId == null) throw new ArgumentNullException(nameof(sessionId));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			SessionId = sessionId;
			UserId = userId;
		}
	}
}
