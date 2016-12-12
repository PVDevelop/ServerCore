using System;
using PVDevelop.UCoach.Domain;

namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate.Events
{
	public class UserCreaed : IDomainEvent
	{
		public string Email { get; }
		public string Password { get; }

		public UserCreaed(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			Email = email;
			Password = password;
		}
	}
}
