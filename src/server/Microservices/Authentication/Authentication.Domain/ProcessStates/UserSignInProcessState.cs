using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.ProcessStates
{
	public class UserSignInProcessState
	{
		public UserAccessToken Token { get; }

		public UserSignInMachineState State { get; }

		public UserSignInProcessState(
			UserSignInMachineState state)
		{
			State = state;
		}

		public UserSignInProcessState(
			UserAccessToken token)
		{
			if (token == null) throw new ArgumentNullException(nameof(token));

			Token = token;
			State = UserSignInMachineState.TokenGenerated;
		}
	}
}
