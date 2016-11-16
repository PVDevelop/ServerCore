using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PVDevelop.UCoach.AuthenticationContrancts.Rest;
using PVDevelop.UCoach.Configuration;
using PVDevelop.UCoach.Rest;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	[Route("api/[controller]")]
	public class ConfirmationsController : Controller
	{
		private readonly IConnectionStringProvider _authenticationEndpointConnectionStringProvider;

		public ConfirmationsController(
			IConnectionStringProvider authenticationEndpointConnectionStringProvider)
		{
			if (authenticationEndpointConnectionStringProvider == null)
				throw new ArgumentNullException(nameof(authenticationEndpointConnectionStringProvider));

			_authenticationEndpointConnectionStringProvider = authenticationEndpointConnectionStringProvider;
		}

		[HttpPost]
		public async Task ConfirmUserAsync([FromBody] ConfirmUserRegistrationDto confirmUserRegistrationDto)
		{
			if (confirmUserRegistrationDto == null) throw new ArgumentNullException(nameof(confirmUserRegistrationDto));
			var tokenDto = await GetAuthenticationUrl().PostJsonWithResultAsync<TokenDto>("api/confirmations", confirmUserRegistrationDto);

			var options = new CookieOptions
			{
				Path = "/",
				Expires = tokenDto.Expiration
			};

			Response.Cookies.Append(TokenConst.ACCESS_TOKEN_COOKIE_NAME, tokenDto.Token, options);
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
