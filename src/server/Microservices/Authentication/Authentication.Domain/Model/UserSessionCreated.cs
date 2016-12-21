using System;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserSessionCreated : IDomainEvent
	{
		public UserSessionId SessionId { get; }

		public UserId UserId { get; }

		public UserSessionCreated(UserSessionId sessionId, UserId userId)
		{
			if (sessionId == null) throw new ArgumentNullException(nameof(sessionId));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			SessionId = sessionId;
			UserId = userId;
		}
	}
}
