using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Events
{
	public class ConfirmationApproved : IDomainEvent
	{
		public UserId UserId { get; }

		public ConfirmationApproved(
			UserId userId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
