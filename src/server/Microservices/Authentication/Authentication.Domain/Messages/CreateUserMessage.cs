using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class CreateUserMessage : ISagaMessage
	{
		public SagaId SagaId { get; }
		public SagaStatus Status => SagaStatus.New;

		public string Email { get; }
		public string Password { get; }

		public CreateUserMessage(SagaId sagaId, string email, string password)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			SagaId = sagaId;
			Email = email;
			Password = password;
		}
	}
}
