using System.Collections.Generic;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserSession : AEventSourcedAggregate<UserSessionId>
	{
		public SessionState State { get; private set; }

		public UserSession(UserSessionId id) : base(id)
		{
		}

		public UserSession(UserSessionId id, int initialVersion, IEnumerable<IDomainEvent> events) : base(id, initialVersion, events)
		{
		}

		protected override void When(IDomainEvent @event)
		{
			throw new System.NotImplementedException();
		}
	}
}
