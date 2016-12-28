using System;
using NUnit.Framework;
using Polly;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Shared.ProcessManagement;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserSignInServiceTests
	{
		[Test]
		public void SignIn_UserDaoReturnsAuthToken()
		{
			using (var authContext = 
				new AuthenticationContextBuilder(new UtcTimeProvider()).
				Build())
			{
				authContext.Start();

				var userRepository = authContext.UserRepository;

				var user = new User(
					new ProcessId(Guid.NewGuid()),
					new UserId(Guid.NewGuid()),
					"mail@mail.ru",
					"P@ssw0rd");

				user.Confirm(new ProcessId(Guid.NewGuid()));

				userRepository.SaveUser(user);

				var sessionReposiory = authContext.UserSessionRepository;

				var session = new UserSession(
					new ProcessId(Guid.NewGuid()),
					new UserSessionId(Guid.NewGuid()),
					user.Id);

				sessionReposiory.SaveSession(session);

				var userSignInService = new UserSignInService(authContext.ProcessManager);
				var processId = userSignInService.SignIn(user.Email, user.Password);

				var userDao = new UserDao(authContext.ProcessManager);

				var token = Policy<UserAccessToken>.
					HandleResult(t => t == null).
					WaitAndRetry(50, i => TimeSpan.FromMilliseconds(100)).
					Execute(() => userDao.GetUserAccessToken(processId));

				Assert.NotNull(token);
			}
		}
	}
}
