using System;
using System.Threading;
using NUnit.Framework;
using Polly;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Model.Confirmation;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class UserConfirmationServiceTests
	{
		[Test]
		public void ConfirmUser_UserDaoReturnsExpectedResult()
		{
			using (var authContext = 
				new AuthenticationContextBuilder(new UtcTimeProvider()).
				Build())
			{
				authContext.Start();

				var createUserProcessId = new ProcessId(Guid.NewGuid());

				var user = new UserAggregate(
					createUserProcessId,
					new UserId(Guid.NewGuid()),
					"some@mail.ru",
					"P@ssw0rd");

				authContext.UserRepository.SaveUser(user);

				var confirmationKey = new ConfirmationKey("SomeConfirmationKey");

				var confirmation = new ConfirmationAggregate(
					createUserProcessId,
					confirmationKey,
					user.Id);

				authContext.ConfirmationRepository.SaveConfirmation(confirmation);

				var userConfirmationService = new UserConfirmationService(authContext.ProcessManager);

				var processId = userConfirmationService.ConfirmUser(confirmationKey);

				var userDao = new UserDao(authContext.ProcessManager);

				var state = Policy<UserConfirmationProcessState>.
					HandleResult(s => s != UserConfirmationProcessState.SessionStarted).
					WaitAndRetry(50, i => TimeSpan.FromMilliseconds(100)).
					Execute(() => userDao.GetUserConfirmationState(processId));

				Assert.AreEqual(UserConfirmationProcessState.SessionStarted, state);
			}
		}
	}
}