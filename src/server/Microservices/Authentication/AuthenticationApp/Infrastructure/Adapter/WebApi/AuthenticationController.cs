using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi.Dto;
using PVDevelop.UCoach.Timing;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi
{
	public class AuthenticationController : Controller
	{
		private const string AccessTokenCookieName = "access_token";

		private readonly IUserService _userService;
		private readonly IUtcTimeProvider _utcTimeProvider;

		public AuthenticationController(
			IUserService userService, 
			IUtcTimeProvider utcTimeProvider)
		{
			if (userService == null) throw new ArgumentNullException(nameof(userService));
			if (utcTimeProvider == null) throw new ArgumentNullException(nameof(utcTimeProvider));

			_userService = userService;
			_utcTimeProvider = utcTimeProvider;
		}

		[HttpPost("api/users")]
		public void CreatUser([FromBody] CreateUserDto createUserDto)
		{
			if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));

			_userService.CreateUser(
				email: createUserDto.Email,
				password: createUserDto.Password);
		}

		[HttpGet("api/confirmations/{key}")]
		public void ConfirmUser(string key)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set", key);

			_userService.ConfirmUserRegistration(key);
		}

		[HttpPut("api/sign_in")]
		public void SignIn([FromBody] UserCredentialsDto userSignInDto)
		{
			if (userSignInDto == null) throw new ArgumentNullException(nameof(userSignInDto));

			var token = _userService.SignIn(userSignInDto.Email, userSignInDto.Password);
			SetAccessToken(token);
		}

		[HttpPut("api/sign_out")]
		public void SignOut()
		{
			var accessToken = GetAccessToken();
			_userService.SignOut(accessToken);
			ExpireToken();
		}

		[HttpPut("api/tokens")]
		public void ValidateCurrentToken()
		{
			var accessToken = GetAccessToken();
			_userService.ValidateToken(accessToken);
		}

		private void SetAccessToken(AccessToken accessToken)
		{
			var cookieOptions = new CookieOptions
			{
				Path = "/",
				Expires = accessToken.Expiration
			};
			var encodedToken = TokenEncoder.Encode(accessToken);
			Response.Cookies.Append(AccessTokenCookieName, encodedToken, cookieOptions);
		}

		private AccessToken GetAccessToken()
		{
			string token;
			if (!Request.Cookies.TryGetValue(AccessTokenCookieName, out token))
			{
				throw new InvalidOperationException();
			}

			return TokenEncoder.Decode(token);
		}

		private void ExpireToken()
		{
			string token;
			if (!Request.Cookies.TryGetValue(AccessTokenCookieName, out token))
			{
				return;
			}

			var accessToken = TokenEncoder.Decode(token);

			var expiredAccessToken = new AccessToken(
				accessToken.UserId, 
				accessToken.Token, 
				_utcTimeProvider.UtcNow.AddDays(-1));

			SetAccessToken(expiredAccessToken);
		}
	}
}
