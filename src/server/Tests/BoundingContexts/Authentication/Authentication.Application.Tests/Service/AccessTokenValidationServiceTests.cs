using System;
using System.Linq;
using NUnit.Framework;
using Polly;
using PVDevelop.UCoach.Application.Service;
using PVDevelop.UCoach.Authentication.Infrastructure.Adapter;
using PVDevelop.UCoach.Domain.Events;
using PVDevelop.UCoach.Domain.Model.Confirmation;
using PVDevelop.UCoach.Domain.Model.User;
using PVDevelop.UCoach.Domain.Model.UserSession;
using PVDevelop.UCoach.Domain.ProcessStates;
using PVDevelop.UCoach.Shared.ProcessManagement;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.Application.Tests.Service
{
	[TestFixture]
	public class AccessTokenValidationServiceTests
	{
		[Test]
		public void ValidateToken_UserDaoReturnsExpectedResult()
		{
			using (var authContext =
				new AuthenticationContextBuilder(new UtcTimeProvider()).
				Build())
			{
				authContext.Start();

				var session = new UserSessionAggregate(
					new ProcessId(Guid.NewGuid()), 
					new UserSessionId(Guid.NewGuid()), 
					new UserId(Guid.NewGuid()));

				session.GenerateToken(new ProcessId(Guid.NewGuid()), DateTime.UtcNow);

				var token = session.Events.OfType<TokenGenerated>().Single().Token;

				authContext.UserSessionRepository.SaveSession(session);

				var tokenValidationSerivce = new AccessTokenValidationService(authContext.ProcessManager);

				var processId = tokenValidationSerivce.ValidateToken(token);

				var userDao = new UserDao(authContext.ProcessManager);

				var state = Policy<AccessTokenValidationProcessState>.
					HandleResult(s => s != AccessTokenValidationProcessState.ValidationApproved).
					WaitAndRetry(10, i => TimeSpan.FromMilliseconds(100)).
					Execute(() => userDao.GetAccessTokenValidationState(processId));

				Assert.AreEqual(AccessTokenValidationProcessState.ValidationApproved, state);
			}
		}
	}
}
