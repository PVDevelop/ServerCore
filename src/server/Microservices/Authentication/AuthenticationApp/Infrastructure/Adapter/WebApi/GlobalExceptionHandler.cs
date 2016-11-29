using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PVDevelop.UCoach.AuthenticationApp.Application;
using PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi.Dto;
using PVDevelop.UCoach.Logging;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi
{
	public class GlobalExceptionHandler : IExceptionFilter
	{
		private readonly ILogger _logger = LoggerHelper.GetLogger<GlobalExceptionHandler>();

		public void OnException(ExceptionContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			if (context.Exception is ApplicationException)
			{
				ProcessApplicationException(context);
			}
			else
			{
				_logger.Error(context.Exception, "Unhandled exception. 500 will be returned.");
			}
		}

		private void ProcessApplicationException(ExceptionContext context)
		{
			var exception = (ApplicationException)context.Exception;
			var result = new ApplicationErrorDto(exception.Message);
			context.Result = new BadRequestObjectResult(result);
		}
	}
}
