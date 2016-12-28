using System;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserSignInService
	{
		private readonly IProcessManager _processManager;

		public UserSignInService(IProcessManager processManager)
		{
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));

			_processManager = processManager;
		}

		public ProcessId SignIn(string email, string password)
		{
			var processId = _processManager.StartProcess(
				AuthProcessStateDescriptionFactory.
					GetUserSignInProcessStateDescriptions().
					ToList());

			_processManager.HandleEvent(new UserSignInRequested(processId, email, password));

			return processId;
		}
	}
}
