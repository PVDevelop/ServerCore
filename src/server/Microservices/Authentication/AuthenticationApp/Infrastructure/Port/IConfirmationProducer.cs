namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure.Port
{
	public interface IConfirmationProducer
	{
		/// <summary>
		/// Доставляет получателю ключ подтверждения.
		/// </summary>
		/// <param name="address">Адресс получателя</param>
		/// <param name="url">Ссылка для подтверждения</param>
		void Produce(string address, string url);
	}
}
