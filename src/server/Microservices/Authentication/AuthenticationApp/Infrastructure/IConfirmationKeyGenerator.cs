namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public interface IConfirmationKeyGenerator
	{
		/// <summary>
		/// Генерация ключа подтверждения.
		/// </summary>
		string Generate();
	}
}
