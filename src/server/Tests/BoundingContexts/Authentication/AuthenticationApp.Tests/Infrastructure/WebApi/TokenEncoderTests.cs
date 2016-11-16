using System;
using Newtonsoft.Json;
using NUnit.Framework;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.WebApi;

namespace PVDevelop.UCoach.AuthenticationApp.Tests.Infrastructure.WebApi
{
	[TestFixture]
	public class TokenEncoderTests
	{
		[Test]
		public void Decode_EncodedTokenString_ValidDecoding()
		{
			var accessTokenBeforeEncoding = new AccessToken("some_user_id", "some_token", DateTime.UtcNow);

			var encodedToken = TokenEncoder.Encode(accessTokenBeforeEncoding);

			var accessTokenAfterEncoding = TokenEncoder.Decode(encodedToken);

			Assert.AreEqual(
				JsonConvert.SerializeObject(accessTokenBeforeEncoding),
				JsonConvert.SerializeObject(accessTokenAfterEncoding));
		}
	}
}
