using System;
using System.Text.RegularExpressions;
using PVDevelop.UCoach.Domain.Exceptions;
using PVDevelop.UCoach.Domain.Messages;

namespace PVDevelop.UCoach.Domain.Model
{
	public sealed class User : AEventSourcedAggregate<UserId>
	{
		public string Email { get; private set; }
		public string Password { get; private set; }

		public User(UserCreatedEvent userCreated) : base(userCreated.UserId)
		{
			ValidateEmail(userCreated.Email);
			ValidatePassword(userCreated.Password);
			Mutate(userCreated);
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

		protected override void When(IDomainEvent @event)
		{
			When((dynamic) @event);
		}

		private void When(UserCreatedEvent userCreated)
		{
			Email = userCreated.Email;
			Password = userCreated.Password;
		}

		private void When(object @event)
		{
			throw new InvalidOperationException($"Unknown event {@event}.");
		}
	}
}
