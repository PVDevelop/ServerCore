using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationTransmittedToPending : IDomainEvent
	{
		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationTransmittedToPending(
			ConfirmationKey confirmationKey,
			UserId userId)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			ConfirmationKey = confirmationKey;
			UserId = userId;
		}
	}
}
