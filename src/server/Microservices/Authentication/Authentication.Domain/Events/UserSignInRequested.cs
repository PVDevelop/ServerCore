using System;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserSignInRequested :
		AProcessEvent
	{
		public string Email { get; }
		public string Password { get; }

		public UserSignInRequested(
			ProcessId processId,
			string email,
			string password)
			: base(processId, new UserSignInProcessState(UserSignInMachineState.SignInRequested))
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set.", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set.", nameof(password));

			Email = email;
			Password = password;
		}
	}
}
