using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public class ProxyMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IConnectionStringProvider _authenticationEndpointConnectionStringProvider;

		public ProxyMiddleware(RequestDelegate next, IConnectionStringProvider authenticationEndpointConnectionStringProvider)
		{
			if (next == null) throw new ArgumentNullException(nameof(next));
			if (authenticationEndpointConnectionStringProvider == null)
				throw new ArgumentNullException(nameof(authenticationEndpointConnectionStringProvider));

			_next = next;
			_authenticationEndpointConnectionStringProvider = authenticationEndpointConnectionStringProvider;
		}

		public async Task Invoke(HttpContext context)
		{
			if (!IsApiRequest(context.Request.Path))
			{
				await _next.Invoke(context);
				return;
			}

			using (var client = new HttpClient())
			using (var requestMessage = CreateRequestMessageFromContext(context))
			using (var responseMessage = await client.SendAsync(requestMessage))
			{
				await CopyResponse(responseMessage, context.Response);
			}
		}

		private static bool IsApiRequest(string path)
		{
			var firstPath = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
			return string.Equals("api", firstPath, StringComparison.OrdinalIgnoreCase);
		}

		private HttpRequestMessage CreateRequestMessageFromContext(HttpContext context)
		{
			var httpMethod = new HttpMethod(context.Request.Method);

			var baseUri = new Uri(_authenticationEndpointConnectionStringProvider.ConnectionString);
			var uri = new Uri(
				baseUri,
				$"{context.Request.Path.Value}{context.Request.QueryString}");

			var requestMessage = new HttpRequestMessage(httpMethod, uri);

			CopyRequestContent(context.Request, requestMessage);
			CopyRequestHeaders(context.Request, requestMessage);

			return requestMessage;
		}

		private static void CopyRequestContent(HttpRequest source, HttpRequestMessage target)
		{
			if (!string.Equals(source.Method, "GET", StringComparison.OrdinalIgnoreCase) &&
				!string.Equals(source.Method, "HEAD", StringComparison.OrdinalIgnoreCase) &&
				!string.Equals(source.Method, "DELETE", StringComparison.OrdinalIgnoreCase) &&
				!string.Equals(source.Method, "TRACE", StringComparison.OrdinalIgnoreCase))
			{
				var streamContent = new StreamContent(source.Body);
				target.Content = streamContent;
			}
		}

		private static void CopyRequestHeaders(HttpRequest source, HttpRequestMessage target)
		{
			foreach (var header in source.Headers)
			{
				if (!target.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
				{
					target.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
				}
			}
		}

		private static async Task CopyResponse(HttpResponseMessage source, HttpResponse target)
		{
			target.StatusCode = (int)source.StatusCode;

			foreach (var header in source.Headers)
			{
				target.Headers[header.Key] = header.Value.ToArray();
			}

			foreach (var header in source.Content.Headers)
			{
				target.Headers[header.Key] = header.Value.ToArray();
			}

			target.Headers.Remove("transfer-encoding");
			await source.Content.CopyToAsync(target.Body);
		}
	}
}
