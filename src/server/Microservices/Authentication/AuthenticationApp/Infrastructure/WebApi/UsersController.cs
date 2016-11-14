using System;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.WebApi
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
		public void CreatUserAsync([FromBody] CreateUserDto createUserDto)
		{
			if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));

			_userService.CreateUser(
				email: createUserDto.Email, 
				password: createUserDto.Password);
		}
	}
}
