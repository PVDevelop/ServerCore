using System;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.UserAggregate;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port;

namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public class UserService2
	{
		private readonly IUserRepository2 _userRepository;

		public UserService2(IUserRepository2 userRepository)
		{
			if (userRepository == null) throw new ArgumentNullException(nameof(userRepository));

			_userRepository = userRepository;
		}

		public void CreateUser(string email, string password)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));
			if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Not set", nameof(password));

			var user = User.New(Guid.NewGuid(), email, password);
			_userRepository.AddUpdate(user);
		}
	}
}
