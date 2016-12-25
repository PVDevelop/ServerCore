using System;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Service;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserRegistrationService
	{
		private readonly IProcessManager _processManager;

		public UserRegistrationService(IProcessManager processManager)
		{
			if (processManager == null) throw new ArgumentNullException(nameof(processManager));
			_processManager = processManager;
		}

		/// <summary>
		/// Регистрирует пользователя, отправляя подтверждение регистрации ему на почту.
		/// </summary>
		/// <param name="email">Почтовый адрес пользователя.</param>
		/// <param name="password">Пароль пользователя.</param>
		public ProcessId RegisterUser(string email, string password)
		{
			var processId = _processManager.StartProcess(
				AuthProcessStateDescriptionFactory.
				GetUserRegistrationProcessStateDescriptions().
				ToList());

			var @startEvent = new RegisterUserRequested(processId, email, password);

			_processManager.HandleEvent(startEvent);

			return processId;
		}
	}
}
