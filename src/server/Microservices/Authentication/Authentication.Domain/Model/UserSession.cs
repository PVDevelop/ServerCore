using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserSession : AEventSourcedAggregate<UserSessionId>
	{
		public SessionState State { get; private set; }

		public UserId UserId { get; private set; }

		public UserSession(UserSessionId sessionId, UserId userId) : base(sessionId)
		{
			Mutate(new UserSessionCreated(sessionId, userId));
		}

		public UserSession(UserSessionId sessionId, int initialVersion, IEnumerable<IDomainEvent> events) : 
			base(sessionId, initialVersion, events)
		{
		}

		protected override void When(IDomainEvent @event)
		{
			When((dynamic) @event);
		}

		private void When(UserSessionCreated @event)
		{
			UserId = @event.UserId;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException();
		}
	}
}
