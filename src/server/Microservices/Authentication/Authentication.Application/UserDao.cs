using System.Collections.Concurrent;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application
{
	public class UserDao : IEventObserver<ConfirmationTransmittedToPending>
	{
		private readonly ConcurrentBag<UserId> _registeredUsers = new ConcurrentBag<UserId>();

		public UserRegistrationStatus GetUserCreationStatus(UserId userId)
		{
			return
				_registeredUsers.Contains(userId)
					? UserRegistrationStatus.Registered
					: UserRegistrationStatus.Pending;
		}

		public void HandleEvent(ConfirmationTransmittedToPending @event)
		{
			_registeredUsers.Add(@event.UserId);
		}
	}
}