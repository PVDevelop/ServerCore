using System;
using NUnit.Framework;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure;
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

			var userSession = new UserSession(new UserSessionGeneratorStub("some_id"), new UtcTimeProviderStub(time));
			var accessToken = userSession.GenerateToken("user");

			Assert.NotNull(accessToken);

			Assert.AreEqual("user", accessToken.UserId);
			var expectedExpiration = time + UserSession.TokenExpirationPeriod;
			Assert.AreEqual(accessToken.Expiration, expectedExpiration);
			Assert.IsTrue(BCrypt.Net.BCrypt.Verify(userSession.Id, accessToken.Token));
		}

		[Test]
		public void Validate_ValidToken_ReturnsTrue()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);
			var userSession = new UserSession(new UserSessionGeneratorStub("some_id"), timeProvider);

			var token = userSession.GenerateToken("user");

			Assert.IsTrue(userSession.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_InvalidToken_ReturnsFalse()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession1 = new UserSession(new UserSessionGeneratorStub("some_id_1"), timeProvider);
			var userSession2 = new UserSession(new UserSessionGeneratorStub("some_id_2"), timeProvider);

			var token = userSession2.GenerateToken("user");

			Assert.IsFalse(userSession1.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_ExpiredToken_ReturnsFalse()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession = new UserSession(new UserSessionGeneratorStub("some_id"), timeProvider);
			var token = userSession.GenerateToken("user");

			timeProvider.UtcNow += UserSession.TokenExpirationPeriod + TimeSpan.FromSeconds(1);

			Assert.IsFalse(userSession.Validate(token, timeProvider));
		}

		[Test]
		public void Validate_ExpiredSession_ReturnsFalse()
		{
			var timeProvider = new UtcTimeProviderStub(DateTime.UtcNow);

			var userSession = new UserSession(new UserSessionGeneratorStub("some_id"), timeProvider);

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

		internal class UserSessionGeneratorStub : IUserSessionGenerator
		{
			private readonly string _id;

			public UserSessionGeneratorStub(string id)
			{
				_id = id;
			}

			public string Generate()
			{
				return _id;
			}
		}
	}
}
