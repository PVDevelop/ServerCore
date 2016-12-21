using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class UserCreatedEvent : ISagaEvent
	{
		public SagaId Id { get; }
		public object Progress { get; }

		public UserId UserId { get; }
		public string Email { get; }
		public string Password { get; }

		public UserCreatedEvent(
			SagaId sagaId, 
			UserCreationProgress progress, 
			UserId userId, 
			string email, 
			string password)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (userId == null) throw new ArgumentNullException(nameof(userId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			Id = sagaId;
			UserId = userId;
			Email = email;
			Password = password;
		}
	}
}
