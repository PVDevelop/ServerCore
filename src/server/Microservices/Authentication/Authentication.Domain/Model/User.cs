using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Exceptions;

namespace PVDevelop.UCoach.Domain.Model
{
	public sealed class User : AEventSourcedAggregate<UserId>
	{
		public string Email { get; private set; }
		public string Password { get; private set; }
		public UserState State { get; private set; }

		public User(UserId userId, string email, string password) : base(userId)
		{
			ValidateEmail(email);
			ValidatePassword(password);

			var userCreated = new UserCreated(userId, email, password);

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

		public void Confirm()
		{
			if (State != UserState.SignedIn)
			{
				Mutate(new UserConfirmed());
			}
		}

		protected override void When(IDomainEvent @event)
		{
			When((dynamic) @event);
		}

		private void When(UserCreated @event)
		{
			Email = @event.Email;
			Password = @event.Password;
		}

		private void When(UserConfirmed @event)
		{
			State = UserState.SignedOut;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
