using System;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi
{
	public class AuthenticationController : Controller
	{
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

		[HttpPost("api/confirmations")]
		public TokenDto ConfirmUser([FromBody] ConfirmUserRegistrationDto confirmUserRegistrationDto)
		{
			if (confirmUserRegistrationDto == null)
				throw new ArgumentNullException(nameof(confirmUserRegistrationDto));

			var token = _userService.ConfirmUserRegistration(confirmUserRegistrationDto.ConfirmationKey);

			var encodedToken = TokenEncoder.Encode(token);

			return new TokenDto(encodedToken, token.Expiration);
		}

		[HttpGet("api/tokens/{token}")]
		public UserProfileDto ValidateToken(string token)
		{
			if (string.IsNullOrWhiteSpace(token)) throw new ArgumentException("Not set", nameof(token));

			var accessToken = TokenEncoder.Decode(token);

			throw new NotImplementedException();
		}
	}
}
