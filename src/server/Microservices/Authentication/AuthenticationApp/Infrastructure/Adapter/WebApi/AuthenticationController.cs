using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationApp.Domain.Model;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi.Dto;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi
{
	public class AuthenticationController : Controller
	{
		private const string AccessTokenCookieName = "access_token";

		private readonly IUserService _userService;

		public AuthenticationController(IUserService userService)
		{
			if (userService == null) throw new ArgumentNullException(nameof(userService));
			_userService = userService;
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

			var token = _userService.ConfirmUserRegistration(key);
			SetAccessToken(token); ;
		}

		[HttpPut("api/users")]
		public void SignIn([FromBody] UserCredentialsDto userSignInDto)
		{
			if (userSignInDto == null) throw new ArgumentNullException(nameof(userSignInDto));

			var token = _userService.SignIn(userSignInDto.Email, userSignInDto.Password);
			SetAccessToken(token);
		}

		[HttpPut("api/tokens")]
		public void ValidateCurrentToken()
		{
			string token;
			if(!Request.Cookies.TryGetValue(AccessTokenCookieName, out token))
			{
				throw new InvalidOperationException();
			}

			var accessToken = TokenEncoder.Decode(token);
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
	}
}
