using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserConfirmationServiceTests
	{
		[Test]
		public void ConfirmUser_UserDaoReturnsExpectedResult()
		{
			var userDao = new UserDao();

			using (var authContext =
				new AuthenticationContextBuiler().
					WithUserRepository().
					WithConfirmationRepository().
					WithUserSessionRepository().
					WithUserConfirmationService().
					WithEventObserver(userDao).
					Build())
			{
				authContext.Start();

				var user = new User(new UserId(Guid.NewGuid()), "some@mail.ru", "P@ssw0rd");
				authContext.UserRepository.SaveUser(user);

				var confirmation = new Confirmation(new ConfirmationKey("SomeConfirmationKey"), user.Id);
				authContext.ConfirmationRepository.SaveConfirmation(confirmation);

				new UserConfirmationService(authContext.ConfirmationRepository).
					ConfirmUser(confirmation.Id);

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var status = userDao.GetUserConfirmationStatus(confirmation.Id);

				Assert.AreEqual(UserConfirmationStatus.Confirmed, status);
			}
		}
	}
}