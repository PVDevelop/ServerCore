using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PVDevelop.UCoach.Configuration;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Email
{
	public class EmailConfirmationProducer : IConfirmationProducer
	{
		private readonly IConfigurationSectionProvider<EmailProducerSettings> _configurationSectionProvider;

		public EmailConfirmationProducer(
			IConfigurationSectionProvider<EmailProducerSettings> configurationSectionProvider)
		{
			if (configurationSectionProvider == null) throw new ArgumentNullException(nameof(configurationSectionProvider));

			_configurationSectionProvider = configurationSectionProvider;
		}

		public void Produce(string address, string url)
		{
			using (var client = new SmtpClient())
			{
				var emailSettings = _configurationSectionProvider.GetSection();

				client.Connect(
					host: emailSettings.SmtpHost,
					port: emailSettings.SmtpPort,
					options: emailSettings.EnableSsl ? SecureSocketOptions.StartTlsWhenAvailable : SecureSocketOptions.None);
				client.Authenticate(emailSettings.UserName, emailSettings.Password);

				var message = new MimeMessage();
				message.From.Add(new MailboxAddress("Some Application", emailSettings.SenderAddress));
				message.To.Add(new MailboxAddress("Someone", address));
				message.Subject = "Confirmation";

				var bodyBuilder = new BodyBuilder { HtmlBody = string.Format(Resources.Confirmation, url) };

				message.Body = bodyBuilder.ToMessageBody();
				client.Send(message);

				client.Disconnect(true);
			}
		}
	}
}
