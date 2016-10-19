using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PVDevelop.UCoach.Rest;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		[HttpPost]
		public async Task CreateUser([FromBody] CreateUserDto createUserDto)
		{
			if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));

			var restClient = new RestClient("http://localhost:8001");
			var authUserDto = MapGatewayUserDtoToAuthUserDto(createUserDto);
			await restClient.PostAsync("api/users", authUserDto);
		}

		private AuthenticationContrancts.Rest.CreateUserDto MapGatewayUserDtoToAuthUserDto(CreateUserDto userDto)
		{
			return new AuthenticationContrancts.Rest.CreateUserDto(userDto.Email, userDto.Password, "some_url");
		}
	}
}
