using System;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.WebApi
{
	[Route("api/[controller]")]
	public class ConfirmationsController : Controller
	{
		private readonly IUserService _userService;

		public ConfirmationsController(IUserService userService)
		{
			if (userService == null) throw new ArgumentNullException(nameof(userService));
			_userService = userService;
		}

		[HttpPost]
		public TokenDto ConfirmUserAsync([FromBody] ConfirmUserRegistrationDto confirmUserRegistrationDto)
		{
			if (confirmUserRegistrationDto == null)
				throw new ArgumentNullException(nameof(confirmUserRegistrationDto));

			var token = _userService.ConfirmUserRegistration(confirmUserRegistrationDto.ConfirmationKey);
			return new TokenDto(token.Token, token.Expiration);
		}
	}
}
