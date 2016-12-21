using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserConfirmed : IDomainEvent
	{
		public UserId UserId { get; }

		public UserConfirmed(UserId userId)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			UserId = userId;
		}
	}
}
