using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserConfirmationService
	{
		private readonly IConfirmationRepository _confirmationRepository;

		public UserConfirmationService(IConfirmationRepository confirmationRepository)
		{
			if (confirmationRepository == null) throw new ArgumentNullException(nameof(confirmationRepository));

			_confirmationRepository = confirmationRepository;
		}

		public void ConfirmUser(ConfirmationKey confirmationKey)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));

			var confirmation = _confirmationRepository.GetConfirmation(confirmationKey);
			confirmation.Confirm();

			_confirmationRepository.SaveConfirmation(confirmation);
		}
	}
}
