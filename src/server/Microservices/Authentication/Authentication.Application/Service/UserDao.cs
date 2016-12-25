using System;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserDao
	{
		private readonly IProcessManager _processManager;

		public UserDao(IProcessManager processManager)
		{
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));

			_processManager = processManager;
		}

		public UserRegistrationProcessState GetUserRegisrationState(ProcessId processId)
		{
			return (UserRegistrationProcessState) _processManager.GetProcessState(processId);
		}

		public UserConfirmationProcessState GetUserConfirmationState(ProcessId processId)
		{
			return (UserConfirmationProcessState) _processManager.GetProcessState(processId);
		}
	}
}