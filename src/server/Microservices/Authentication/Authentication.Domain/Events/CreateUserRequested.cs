using System;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Events
{
	public class CreateUserRequested : ISagaEvent
	{
		public SagaId Id { get; }
		public SagaStatus Status => SagaStatus.Pending;

		public string Email { get; }
		public string Password { get; }

		public CreateUserRequested(
			SagaId sagaId, 
			string email, 
			string password)
		{
			if (sagaId == null) throw new ArgumentNullException(nameof(sagaId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			Id = sagaId;
			Email = email;
			Password = password;
		}
	}
}
