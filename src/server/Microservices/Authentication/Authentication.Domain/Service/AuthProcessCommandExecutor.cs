using System;
using PVDevelop.UCoach.Domain.Commands;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;
using PVDevelop.UCoach.Shared.ProcessManagement;

namespace PVDevelop.UCoach.Domain.Service
{
	public class AuthProcessCommandExecutor : IProcessCommandExecutor
	{
		private readonly IUserRepository _userRepository;
		private readonly IConfirmationRepository _confirmationRepository;

		public AuthProcessCommandExecutor(
			IUserRepository userRepository,
			IConfirmationRepository confirmationRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));

			_userRepository = userRepository;
			_confirmationRepository = confirmationRepository;
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

		private void DoExecute(object command)
		{
			throw new InvalidOperationException($"Unknown command '{command}'.");
		}
	}
}
