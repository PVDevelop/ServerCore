using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserCreated :
		AProcessEvent,
		IDomainEvent
	{
		public UserId UserId { get; }
		public string Email { get; }
		public string Password { get; }

		public UserCreated(
			ProcessId processId,
			UserId userId,
			string email,
			string password) :
			base(processId, UserRegistrationProcessState.UserCreated)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			UserId = userId;
			Email = email;
			Password = password;
		}
	}
}
