using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public class IndexSelectorMiddleware
	{
		private readonly RequestDelegate _next;

		public IndexSelectorMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			context.Request.Path = "/index.html";
			await _next.Invoke(context);
		}
	}
}
