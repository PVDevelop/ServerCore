using System;
using NUnit.Framework;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model.Exceptions;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Tests.Domain.Model
{
	[TestFixture]
	public class UserSessionTests
	{
		[Test]
		public void Activate_NewSession_InitialStateIsInactive()
		{
			var session = new UserSession("some");

			Assert.AreEqual(SessionState.Inactive, session.State);
		}

		[Test]
		public void Activate_NewSession_TransmitsToActive()
		{
			var session = new UserSession("some");
			session.Activate();

			Assert.AreEqual(SessionState.Active, session.State);
		}

		[Test]
		public void GenerateToken_GeneratesValidToken()
		{
			var time = DateTime.UtcNow;

			var userSession = CreateActiveSession("userId");
			var accessToken = userSession.GenerateToken(time);

			Assert.NotNull(accessToken);

			Assert.AreEqual("userId", accessToken.UserId);
			var expectedExpiration = time + UserSession.TokenExpirationPeriod;
			Assert.AreEqual(accessToken.Expiration, expectedExpiration);
			Assert.IsTrue(BCrypt.Net.BCrypt.Verify(userSession.Id, accessToken.Token));
		}

		[Test]
		public void Validate_ValidToken_DeosNotThrowException()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);
			var userSession = CreateActiveSession();

			var token = userSession.GenerateToken(timeProvider.UtcNow);

			userSession.Validate(token, timeProvider);
		}

		[Test]
		public void Validate_InvalidToken_ThrowsException()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession1 = CreateActiveSession("some1");
			var userSession2 = CreateActiveSession("some2");

			var token = userSession2.GenerateToken(timeProvider.UtcNow);

			Assert.Throws<InvalidTokenException>(() => userSession1.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_ExpiredToken_ThrowsException()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession = CreateActiveSession();
			var token = userSession.GenerateToken(timeProvider.UtcNow);

			timeProvider.UtcNow += UserSession.TokenExpirationPeriod + TimeSpan.FromSeconds(1);

			Assert.Throws<InvalidTokenException>(() => userSession.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_SessionIsNotActive_ThrowsException()
		{
			var userSession = new UserSession("userId");

			Assert.Throws<InactiveSessionException>(() => userSession.Validate(
				new AccessToken("userId", "some_token", DateTime.UtcNow),
				new UtcTimeProviderStub(DateTime.UtcNow)));
		}

		private UserSession CreateActiveSession(string id = "some")
		{
			var session = new UserSession(id);
			session.Activate();
			return session;
		}

		internal class UtcTimeProviderStub : IUtcTimeProvider
		{
			public DateTime UtcNow { get; set; }

			public UtcTimeProviderStub(DateTime time)
			{
				UtcNow = time;
			}
		}
	}
}
