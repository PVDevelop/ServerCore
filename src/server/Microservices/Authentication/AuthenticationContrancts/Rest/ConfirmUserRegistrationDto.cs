using System;

namespace PVDevelop.UCoach.AuthenticationContrancts.Rest
{
	public class ConfirmUserRegistrationDto
	{
		public string ConfirmationKey { get; }

		public ConfirmUserRegistrationDto(string confirmationKey)
		{
			if (string.IsNullOrWhiteSpace(confirmationKey)) throw new ArgumentException("Not set", nameof(confirmationKey));

			ConfirmationKey = confirmationKey;
		}
	}
}
