using System;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserConfirmationService
	{
		private readonly IProcessManager _processManager;

		public UserConfirmationService(IProcessManager processManager)
		{
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));

			_processManager = processManager;
		}

		public ProcessId ConfirmUser(ConfirmationKey confirmationKey)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));

			var processId = _processManager.StartProcess(
				AuthProcessStateDescriptionFactory.
				GetUserConfirmationProcessStateDescriptions().
				ToList());

			var @event = new ConfirmUserRequested(processId, confirmationKey);

			_processManager.HandleEvent(@event);

			return processId;
		}
	}
}
