namespace PVDevelop.UCoach.Authentication.Infrastructure
{
	public interface IKeyGeneratorService
	{
		/// <summary>
		/// Генерация ключа для подтверждения
		/// </summary>
		string GenerateConfirmationKey();

		/// <summary>
		/// Генерация ключа для токена
		/// </summary>
		string GenerateTokenKey();

		/// <summary>
		/// Генерация Id для пользователя
		/// </summary>
		string GenerateUserId();
	}
}
