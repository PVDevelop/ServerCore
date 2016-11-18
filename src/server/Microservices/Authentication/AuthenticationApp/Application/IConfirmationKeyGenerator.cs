namespace PVDevelop.UCoach.AuthenticationApp.Application
{
	public interface IConfirmationKeyGenerator
	{
		/// <summary>
		/// Генерация ключа подтверждения.
		/// </summary>
		string Generate();
	}
}
