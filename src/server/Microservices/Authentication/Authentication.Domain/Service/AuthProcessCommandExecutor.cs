using System;
using System.Linq;
using PVDevelop.UCoach.Domain.Commands;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.ProcessManagement;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Domain.Service
{
	public class AuthProcessCommandExecutor : IProcessCommandExecutor
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;
		private readonly IUserSessionRepository _userSessionRepository;
		private readonly IUtcTimeProvider _utcTimeProvider;

		public AuthProcessCommandExecutor(
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository,
			IUserSessionRepository userSessionRepository,
			IUtcTimeProvider utcTimeProvider)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));
			if (userSessionRepository == null) throw new ArgumentNullException(nameof(userSessionRepository));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));

			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
			_userSessionRepository = userSessionRepository;
			_utcTimeProvider = utcTimeProvider;
		}

		public void Execute(IProcessCommand command)
		{
			DoExecute((dynamic) command);
		}

		private void DoExecute(CreateUser command)
		{
			var userId = new UserId(Guid.NewGuid());
			var user = new User(command.ProcessId, userId, command.Email, command.Password);
			_userRepository.SaveUser(user);
		}

		private void DoExecute(CreateConfrimation command)
		{
			var confirmationKey = new ConfirmationKey(Guid.NewGuid().ToString());
			var confirmation = new Confirmation(command.ProcessId, confirmationKey, command.UserId);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void DoExecute(ApproveConfirmation command)
		{
			var confirmation = _confirmationRepository.GetConfirmation(command.ConfirmationKey);
			confirmation.Confirm(command.ProcessId);
			_confirmationRepository.SaveConfirmation(confirmation);
		}

		private void DoExecute(ConfirmUser command)
		{
			var user = _userRepository.GetUserById(command.UserId);
			user.Confirm(command.ProcessId);
			_userRepository.SaveUser(user);
		}

		private void DoExecute(StartSession command)
		{
			var userSessionId = new UserSessionId(Guid.NewGuid());
			var userSession = new UserSession(command.ProcessId, userSessionId, command.UserId);
			_userSessionRepository.SaveSession(userSession);
		}

		private void DoExecute(SignIn command)
		{
			var user = _userRepository.GetUserByEmail(command.Email);
			user.SignIn(command.ProcessId, command.Password);
			_userRepository.SaveUser(user);
		}

		private void DoExecute(GenerateToken command)
		{
			var session = _userSessionRepository.GetSessions(command.UserId).Single();
			session.GenerateToken(command.ProcessId, _utcTimeProvider.UtcNow);
			_userSessionRepository.SaveSession(session);
		}

		private void DoExecute(object command)
		{
			throw new InvalidOperationException($"Unknown command '{command}'.");
		}
	}
}
