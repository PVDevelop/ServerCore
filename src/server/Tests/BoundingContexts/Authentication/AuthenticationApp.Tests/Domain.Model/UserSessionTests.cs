using System;
using NUnit.Framework;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Tests.Domain.Model
{
	[TestFixture]
	public class UserSessionTests
	{
		[Test]
		public void GenerateToken_GeneratesValidToken()
		{
			var time = DateTime.UtcNow;

			var userSession = new UserSession("userId", "sessionId", time);
			var accessToken = userSession.GenerateToken();

			Assert.NotNull(accessToken);

			Assert.AreEqual("userId", accessToken.UserId);
			var expectedExpiration = time + UserSession.TokenExpirationPeriod;
			Assert.AreEqual(accessToken.Expiration, expectedExpiration);
			Assert.IsTrue(BCrypt.Net.BCrypt.Verify(userSession.Id, accessToken.Token));
		}

		[Test]
		public void Validate_ValidToken_ReturnsTrue()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);
			var userSession = new UserSession("usId", "sessId", timeProvider.UtcNow);

			var token = userSession.GenerateToken();

			Assert.IsTrue(userSession.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_InvalidToken_ReturnsFalse()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession1 = new UserSession("userId", "some_id_1", timeProvider.UtcNow);
			var userSession2 = new UserSession("userId2", "some_id_2", timeProvider.UtcNow);

			var token = userSession2.GenerateToken();

			Assert.IsFalse(userSession1.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_ExpiredToken_ReturnsFalse()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession = new UserSession("userId", "some_id", timeProvider.UtcNow);
			var token = userSession.GenerateToken();

			timeProvider.UtcNow += UserSession.TokenExpirationPeriod + TimeSpan.FromSeconds(1);

			Assert.IsFalse(userSession.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_ExpiredSession_ReturnsFalse()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession = new UserSession("userId", "some_id", timeProvider.UtcNow);

			var salt = BCrypt.Net.BCrypt.GenerateSalt();
			var token = BCrypt.Net.BCrypt.HashPassword(userSession.Id, salt);
			var accessToken = new AccessToken("user", token, timeProvider.UtcNow.AddDays(1000000));

			timeProvider.UtcNow += UserSession.TokenExpirationPeriod + TimeSpan.FromSeconds(1);

			Assert.IsFalse(userSession.Validate(accessToken, timeProvider));
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
