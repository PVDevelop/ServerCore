using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Messages
{
	public class CreateUserMessage : ISagaMessage
	{
		public Guid SagaId { get; }
		public SagaStatus Status => SagaStatus.Progress;

		public string Email { get; }
		public string Password { get; }

		public CreateUserMessage(Guid sagaId, string email, string password)
		{
			if (sagaId == default(Guid)) throw new ArgumentException("Not set", nameof(sagaId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			SagaId = sagaId;
			Email = email;
			Password = password;
		}
	}
}
