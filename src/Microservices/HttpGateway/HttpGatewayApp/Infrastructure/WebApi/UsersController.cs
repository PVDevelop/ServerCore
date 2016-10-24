using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.Rest;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	[Route("api/[controller]")]
	public class UsersController : Controller
	{
		private readonly IConnectionStringProvider _authenticationEndpointConnectionStringProvider;

		public UsersController(IConnectionStringProvider authenticationEndpointConnectionStringProvider)
		{
			if (authenticationEndpointConnectionStringProvider == null)
				throw new ArgumentNullException(nameof(authenticationEndpointConnectionStringProvider));

			_authenticationEndpointConnectionStringProvider = authenticationEndpointConnectionStringProvider;
		}

		[HttpPost]
		public async Task CreateUser([FromBody] CreateUserDto createUserDto)
		{
			if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));
			await GetAuthenticationUrl().PostJsonAsync("api/users", createUserDto);
		}

		private RestClient GetAuthenticationUrl()
		{
			var url = _authenticationEndpointConnectionStringProvider.ConnectionString;

			if (string.IsNullOrWhiteSpace(url))
			{
				throw new InvalidOperationException("Authentication url is not configured");
			}

			return new RestClient(url);
		}
	}
}
