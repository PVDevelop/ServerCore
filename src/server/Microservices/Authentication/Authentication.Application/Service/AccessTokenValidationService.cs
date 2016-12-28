using System;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Service
{
	public class AccessTokenValidationService
	{
		private readonly IProcessManager _processManager;

		public AccessTokenValidationService(IProcessManager processManager)
		{
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));

			_processManager = processManager;
		}

		public ProcessId ValidateToken(UserAccessToken token)
		{
			if (token == null) throw new ArgumentNullException(nameof(token));

			var processId = _processManager.StartProcess(
				AuthProcessStateDescriptionFactory.
					GetTokenValidationStateDescriptions().
					ToList());

			_processManager.HandleEvent(new TokenValidationRequested(processId, token));

			return processId;
		}
	}
}
