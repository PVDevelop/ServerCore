using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PVDevelop.UCoach.HttpGatewayApp.Infrastructure.WebApi
{
	public class UiBinariesSelectorMiddleware
	{
		private readonly RequestDelegate _next;

		public UiBinariesSelectorMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var extension = System.IO.Path.GetExtension(context.Request.Path);
			if(string.IsNullOrWhiteSpace(extension))
			{
				// это не запрос какого-либо из файлов, значит возвращаем стратовый файл (Index.html)
				context.Request.Path = "/index.html";
			}
			else
			{
				// запрос определенного файла. 
				// При запросе вида /confirmations/some_confirmation, 
				// возвращается index.html, затем клиент посылает запрос вида confirmations/app.js, 
				// и этот файл не может быть найден
				var fileName = System.IO.Path.GetFileName(context.Request.Path);
				context.Request.Path = $"/{fileName}";
			}
			await _next.Invoke(context);
		}
	}
}
