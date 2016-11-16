using System;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.WebApi
{
	[Route("api/[controller]")]
	public class TokensController
	{
		private readonly IUserService _userService;

		public TokensController(IUserService userService)
		{
			if (userService == null) throw new ArgumentNullException(nameof(userService));

			_userService = userService;
		}

		[HttpGet("{token}")]
		public UserProfileDto ValidateToken(string token)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));

			var accessToken = TokenEncoder.Decode(token);

			var email = _userService.ValidateToken(accessToken);

			return new UserProfileDto(email);
		}
	}
}
