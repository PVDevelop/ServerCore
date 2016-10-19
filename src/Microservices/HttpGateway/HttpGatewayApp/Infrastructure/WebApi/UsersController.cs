using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.Rest;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IConfigurationRoot _configurationRoot;

		public UsersController(IConfigurationRoot configurationRoot)
		{
			_configurationRoot = configurationRoot;
		}

		[HttpPost]
		public async Task CreateUser([FromBody] CreateUserDto createUserDto)
		{
			if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));
			await GetAuthenticationUrl().PostJsonAsync("api/users", createUserDto);
		}

		private RestClient GetAuthenticationUrl()
		{
			var url = _configurationRoot.GetConnectionString("AuthenticationUrl");

			if (string.IsNullOrWhiteSpace(url))
			{
				throw new InvalidOperationException("Authentication url is not configured");
			}

			return new RestClient(url);
		}
	}
}
