using System;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Adapter.WebApi.Dto
{
	public class ApplicationErrorDto
	{
		public string Message { get; }

		public ApplicationErrorDto(string message)
		{
			if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Not set", nameof(message));

			Message = message;
		}
	}
}
