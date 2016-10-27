namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Email
{
	public class EmailProducerSettings
	{
		public bool EnableSsl { get; set; }
		public string Password { get; set; }
		public string SenderAddress { get; set; }
		public string SmtpHost { get; set; }
		public int SmtpPort { get; set; }
		public string UserName { get; set; }
	}
}
