using System;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserSignOutService
	{
		private readonly IProcessManager _processManager;

		public UserSignOutService(IProcessManager processManager)
		{
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));

			_processManager = processManager;
		}

		public ProcessId SignOut(UserId userId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			var processId = _processManager.StartProcess(
				AuthProcessStateDescriptionFactory.
					GetUserSignOutProcessStateDescriptions().
					ToList());

			_processManager.HandleEvent(new UserSignOutRequested(processId, userId));

			return processId;
		}
	}
}
