using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PVDevelop.UCoach.Rest;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public class UsersController : Controller
	{
		private const string ACCESS_TOKEN_COOKIE_NAME = "access_token";
		private readonly IConnectionStringProvider _authenticationEndpointConnectionStringProvider;

		public UsersController(IConnectionStringProvider authenticationEndpointConnectionStringProvider)
		{
			if (authenticationEndpointConnectionStringProvider == null)
				throw new ArgumentNullException(nameof(authenticationEndpointConnectionStringProvider));

			_authenticationEndpointConnectionStringProvider = authenticationEndpointConnectionStringProvider;
		}

		[HttpPost("api/users")]
		public async Task CreateUserAsync([FromBody] CreateUserDto createUserDto)
		{
			if (createUserDto == null) throw new ArgumentNullException(nameof(createUserDto));
			await GetAuthenticationUrl().PostJsonAsync("api/users", createUserDto);
		}

		[HttpPost("api/confirmations")]
		public async Task ConfirmUserAsync([FromBody] ConfirmUserRegistrationDto confirmUserRegistrationDto)
		{
			if (confirmUserRegistrationDto == null) throw new ArgumentNullException(nameof(confirmUserRegistrationDto));
			var tokenDto = await GetAuthenticationUrl().PostJsonWithResultAsync<TokenDto>("api/confirmations", confirmUserRegistrationDto);

			var options = new CookieOptions
			{
				Path = "/",
				Expires = tokenDto.Expiration
			};

			Response.Cookies.Append(ACCESS_TOKEN_COOKIE_NAME, tokenDto.Token, options);
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
