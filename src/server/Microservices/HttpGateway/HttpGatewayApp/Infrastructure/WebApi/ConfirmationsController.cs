﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
		public async Task CreateUser([FromBody] ConfirmUserRegistrationDto confirmUserRegistrationDto)
		{
			if (confirmUserRegistrationDto == null) throw new ArgumentNullException(nameof(confirmUserRegistrationDto));
			await GetAuthenticationUrl().PostJsonAsync("api/confirmations", confirmUserRegistrationDto);
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
