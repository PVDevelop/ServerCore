using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PVDevelop.UCoach.Domain.Exceptions;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Domain.SagaProgress;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Model
{
	public sealed class User : AEventSourcedAggregate<UserId>
	{
		public string Email { get; private set; }
		public string Password { get; private set; }
		public UserState State { get; private set; }

		public User(UserCreatedEvent userCreated) : base(userCreated.UserId)
		{
			ValidateEmail(userCreated.Email);
			ValidatePassword(userCreated.Password);
			Mutate(userCreated);
		}

		public User(UserId userId, int initialVersion, IEnumerable<IDomainEvent> domainEvents)
			: base(userId, initialVersion, domainEvents)
		{
		}

		//private static string EncryptPassword(string plainPassword)
		//{
		//	var salt = BCrypt.Net.BCrypt.GenerateSalt();
		//	var password = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
		//	return password;
		//}

		private static void ValidateEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));

			if (!Regex.IsMatch(email, @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$", RegexOptions.IgnoreCase))
			{
				throw new InvalidEmailFormatException(email);
			}
		}

		private static void ValidatePassword(string password)
		{
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", password);

			if (!Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,25}$", RegexOptions.IgnoreCase))
			{
				throw new InvalidPasswordFormatException();
			}
		}

		public void Confirm(SagaId sagaId)
		{
			if (State != UserState.SignedIn)
			{
				Mutate(new UserConfirmedEvent(
					sagaId, 
					new UserConfirmationProgress(UserConfirmationStatus.Pending)));
			}
		}

		protected override void When(IDomainEvent @event)
		{
			When((dynamic) @event);
		}

		private void When(UserCreatedEvent @event)
		{
			Email = @event.Email;
			Password = @event.Password;
		}

		private void When(UserConfirmedEvent @event)
		{
			State = UserState.SignedOut;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
