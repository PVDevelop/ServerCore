using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Email
{
	public class EmailConfirmationProducer : IConfirmationProducer
	{
		private readonly IOptions<EmailProducerSettings> _settings;

		public EmailConfirmationProducer(
			IOptions<EmailProducerSettings> settings)
		{
			if (settings == null) throw new ArgumentNullException(nameof(settings));

			_settings = settings;
		}

		public void Produce(string address, string url)
		{
			using (var client = new SmtpClient())
			{
				var emailSettings = _settings.Value;

				client.Connect(
					host: emailSettings.SmtpHost, 
					port: emailSettings.SmtpPort,
					options:emailSettings.EnableSsl ? SecureSocketOptions.StartTlsWhenAvailable: SecureSocketOptions.None);
				client.Authenticate(emailSettings.UserName, emailSettings.Password);

				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("Some Application", emailSettings.SenderAddress));
				message.To.Add(new MailboxAddress("Someone", address));
				message.Subject = "Confirmation";
				message.Body = new TextPart("plain")
				{
					Text = $"Confirm user creation: {url}"
				};
				client.Send(message);

				client.Disconnect(true);
			}
		}
	}
}
