using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationCreated : IDomainEvent
	{
		public ConfirmationKey ConfirmationKey { get; }
		public UserId UserId { get; }

		public ConfirmationCreated(
			ConfirmationKey confirmationKey, 
			UserId userId)
		{
			if (confirmationKey == null) throw new ArgumentNullException(nameof(confirmationKey));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
			ConfirmationKey = confirmationKey;
		}
	}
}
