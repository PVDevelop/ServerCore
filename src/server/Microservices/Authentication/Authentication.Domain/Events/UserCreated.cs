using System;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Events
{
	public class UserCreated : IDomainEvent
	{
		public UserId UserId { get; }
		public string Email { get; }
		public string Password { get; }

		public UserCreated(
			UserId userId, 
			string email, 
			string password)
		{
			if (userId == null) throw new ArgumentNullException(nameof(userId));
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			UserId = userId;
			Email = email;
			Password = password;
		}
	}
}
