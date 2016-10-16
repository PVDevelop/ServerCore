using System;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.Authentication.Application;

namespace PVDevelop.UCoach.Authentication.Adapter.WebApi
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			if (userService == null) throw new ArgumentNullException(nameof(userService));
			_userService = userService;
		}

		[HttpPost]
		public void CreatUser([FromBody] CreateUserDto createUserDto)
		{
			_userService.CreateUser(
				email: createUserDto.Email, 
				password: createUserDto.Password,
				url4Confirmation: createUserDto.ConfirmationUrl);
		}
	}
}
