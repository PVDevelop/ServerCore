using System;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class CreateUserMessage : ISagaEvent
	{
		public SagaId Id { get; }
		public object Progress { get; }

		public string Email { get; }
		public string Password { get; }

		public CreateUserMessage(
			SagaId sagaId, 
			UserCreationProgress progress,
			string email, 
			string password)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (progress == null) throw new ArgumentNullException(nameof(progress));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			Id = sagaId;
			Progress = progress;
			Email = email;
			Password = password;
		}
	}
}
