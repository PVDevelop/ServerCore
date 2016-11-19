using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
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

			var cookieOptions = new CookieOptions
			{
				Path = "/",
				Expires = token.Expiration
			};
			var encodedToken = TokenEncoder.Encode(token);
			Response.Cookies.Append(AccessTokenCookieName, encodedToken, cookieOptions);
		}
	}
}
