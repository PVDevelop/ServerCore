using System;
using System.Collections.Generic;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Model.UserSession
{
	public class UserSessionAggregate : AEventSourcedAggregate<UserSessionId>
	{
		internal static readonly TimeSpan TokenExpirationPeriod = TimeSpan.FromDays(1);

		public UserSessionState State { get; private set; }

		public UserId UserId { get; private set; }

		public List<UserAccessToken> GeneratedTokens { get; private set; }

		public UserSessionAggregate(
			ProcessId processId,
			UserSessionId sessionId,
			UserId userId) : base(sessionId)
		{
			Mutate(new SessionStarted(processId, sessionId, userId));
		}

		public UserSessionAggregate(UserSessionId sessionId, int initialVersion, IEnumerable<IDomainEvent> events) :
			base(sessionId, initialVersion, events)
		{
		}

		public void GenerateToken(ProcessId processId, DateTime utcNow)
		{
			if (processId == null) throw new ArgumentNullException(nameof(processId));

			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var token = BCrypt.Net.BCrypt.HashPassword(Id.ToString(), salt);

			var accessToken = new UserAccessToken(UserId, token, utcNow + TokenExpirationPeriod);
			Mutate(new TokenGenerated(processId, accessToken));
		}

		protected override void When(IDomainEvent @event)
		{
			ApplyEvent((dynamic) @event);
		}

		private void ApplyEvent(SessionStarted @event)
		{
			UserId = @event.UserId;
			State = UserSessionState.Inactive;
			GeneratedTokens = new List<UserAccessToken>();
		}

		private void ApplyEvent(TokenGenerated @event)
		{
			GeneratedTokens.Add(@event.Token);
		}

		private void ApplyEvent(object @event)
		{
			throw new InvalidOperationException();
		}
	}
}
