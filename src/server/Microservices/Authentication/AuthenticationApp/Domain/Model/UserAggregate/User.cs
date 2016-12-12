using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate.Events;
using PVDevelop.UCoach.Domain;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate
{
	public sealed class User : AEventSourcedAggregate
	{
		public static User New(Guid id, string email, string password)
		{
			return new User(id: id, email: email, password: password);
		}

		internal static User Restore(Guid id, int version, IEnumerable<IDomainEvent> domainEvents)
		{
			return new User(id, version, domainEvents);
		}

		public string Email { get; private set; }
		public string Password { get; private set; }

		private User(Guid id, string email, string password) : base(id)
		{
			ValidateEmail(email);
			ValidatePassword(password);
			Mutate(new UserCreaed(email, EncryptPassword(password)));
		}

		private User(Guid id, int version, IEnumerable<IDomainEvent> domainEvents) :
			base(id, version, domainEvents)
		{
		}

		private static string EncryptPassword(string plainPassword)
		{
			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var password = BCrypt.Net.BCrypt.HashPassword(plainPassword, salt);
			return password;
		}

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

		private void When(UserCreaed userCreated)
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
