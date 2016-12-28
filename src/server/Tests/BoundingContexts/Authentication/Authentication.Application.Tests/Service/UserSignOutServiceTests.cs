using System;
using NUnit.Framework;
using Polly;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserSignOutServiceTests
	{
		[Test]
		public void SignOut_UserDaoReturnsExpectedResult()
		{
			using (var authContext =
				new AuthenticationContextBuilder(new UtcTimeProvider()).
				Build())
			{
				authContext.Start();

				var userId = new UserId(Guid.NewGuid());

				var user = new UserAggregate(
					new ProcessId(Guid.NewGuid()),
					userId,
					"some@mail.ru",
					"P@ssw0rd");

				user.SignIn(new ProcessId(Guid.NewGuid()), "P@ssw0rd");

				authContext.UserRepository.SaveUser(user);

				var session = new UserSessionAggregate(
					new ProcessId(Guid.NewGuid()),
					new UserSessionId(Guid.NewGuid()),
					userId);

				authContext.UserSessionRepository.SaveSession(session);

				var userSignOutService = new UserSignOutService(authContext.ProcessManager);

				var processId = userSignOutService.SignOut(userId);

				var userDao = new UserDao(authContext.ProcessManager);

				var state = Policy<UserSignOutProcessState>.
					HandleResult(s => s != UserSignOutProcessState.SessionDeactivated).
					WaitAndRetry(50, i => TimeSpan.FromMilliseconds(100)).
					Execute(() => userDao.GetUserSignOutProcessState(processId));

				Assert.AreEqual(UserSignOutProcessState.SessionDeactivated, state);
			}
		}
	}
}
