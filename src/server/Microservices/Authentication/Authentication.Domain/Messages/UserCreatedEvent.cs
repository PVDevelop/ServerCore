using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class UserCreatedEvent : ISagaMessage, IDomainEvent
	{
		public SagaId SagaId { get; }
		public SagaStatus Status => SagaStatus.Progress;

		public UserId UserId { get; }
		public string Email { get; }
		public string Password { get; }

		public UserCreatedEvent(SagaId sagaId, UserId userId, string email, string password)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (userId == null) throw new ArgumentNullException(nameof(userId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			SagaId = sagaId;
			UserId = userId;
			Email = email;
			Password = password;
		}
	}
}
