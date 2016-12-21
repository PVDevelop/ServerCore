using System.Collections.Concurrent;
using System.Linq;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.Observing;

namespace PVDevelop.UCoach.Application.Service
{
	public class UserDao :
		IEventObserver<ConfirmationTransmittedToPending>,
		IEventObserver<ConfirmationApproved>,
		IEventObserver<UserSessionCreated>
	{
		private readonly ConcurrentBag<UserId> _registeredUsers = new ConcurrentBag<UserId>();
		private readonly ConcurrentBag<UserId> _confiremdUsers = new ConcurrentBag<UserId>();

		private readonly ConcurrentDictionary<ConfirmationKey, UserId> _userConfirmations =
			new ConcurrentDictionary<ConfirmationKey, UserId>();

		public UserRegistrationStatus GetUserCreationStatus(UserId userId)
		{
			return
				_registeredUsers.Contains(userId)
					? UserRegistrationStatus.Registered
					: UserRegistrationStatus.Pending;
		}

		public UserConfirmationStatus GetUserConfirmationStatus(ConfirmationKey confirmationKey)
		{
			UserId userId;
			if (!_userConfirmations.TryGetValue(confirmationKey, out userId))
			{
				return UserConfirmationStatus.Pending;
			}

			return
				_confiremdUsers.Contains(userId)
					? UserConfirmationStatus.Confirmed
					: UserConfirmationStatus.Pending;
		}

		public void HandleEvent(ConfirmationTransmittedToPending @event)
		{
			_registeredUsers.Add(@event.UserId);
		}

		public void HandleEvent(ConfirmationApproved @event)
		{
			_userConfirmations[@event.ConfirmationKey] = @event.UserId;
		}

		public void HandleEvent(UserSessionCreated @event)
		{
			_confiremdUsers.Add(@event.UserId);
		}
	}
}