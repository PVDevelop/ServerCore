using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserRegistrationServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			var userDao = new UserDao();

			using (var authContext = new AuthenticationContextBuiler().
				WithUserRepository().
				WithConfirmationRepository().
				WithUserRegistrationService(new FakeConfirmationSender()).
				WithEventObserver(userDao).
				Build())
			{
				authContext.Start();

				var userId = new UserId(Guid.NewGuid());
				new UserRegistrationService(authContext.UserRepository).
					RegisterUser(userId, "some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var status = userDao.GetUserCreationStatus(userId);

				Assert.AreEqual(UserRegistrationStatus.Registered, status);
			}
		}

		private class FakeConfirmationSender : IConfirmationSender
		{
			public void Send(ConfirmationKey confirmationKey)
			{
			}
		}
	}
}