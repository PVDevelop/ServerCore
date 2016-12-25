using System;
using System.Threading;
using NUnit.Framework;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.ProcessStates;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserRegistrationServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			using (var authContext = new AuthenticationContextBuilder().Build())
			{
				authContext.Start();

				var userRegistrationService = new UserRegistrationService(authContext.ProcessManager);
				var processId = userRegistrationService.RegisterUser("some@mail.ru", "P@ssw0rd");

				Thread.Sleep(TimeSpan.FromSeconds(5));

				var userDao = new UserDao(authContext.ProcessManager);
				var state = userDao.GetUserCreationState(processId);

				Assert.AreEqual(UserRegistrationProcessState.ConfirmationCreated, state);
			}
		}
	}
}