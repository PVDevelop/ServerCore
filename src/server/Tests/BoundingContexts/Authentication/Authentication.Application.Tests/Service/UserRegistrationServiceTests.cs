using System;
using System.Threading;
using NUnit.Framework;
using Polly;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserRegistrationServiceTests
	{
		[Test]
		public void RegisterUser_UserDaoReturnsExpectedResult()
		{
			using (var authContext = 
				new AuthenticationContextBuilder(new UtcTimeProvider()).
				Build())
			{
				authContext.Start();

				var userRegistrationService = new UserRegistrationService(authContext.ProcessManager);
				var processId = userRegistrationService.RegisterUser("some@mail.ru", "P@ssw0rd");

				var userDao = new UserDao(authContext.ProcessManager);

				var state = Policy<UserRegistrationProcessState>.
					HandleResult(s => s != UserRegistrationProcessState.ConfirmationCreated).
					WaitAndRetry(50, i => TimeSpan.FromMilliseconds(100)).
					Execute(() => userDao.GetUserRegisrationState(processId));

				Assert.AreEqual(UserRegistrationProcessState.ConfirmationCreated, state);
			}
		}
	}
}