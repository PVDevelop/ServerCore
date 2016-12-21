using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserCreated : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Pending;

		public UserId UserId { get; }
		public string Email { get; }
		public string Password { get; }

		public UserCreated(
			SagaId sagaId, 
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
