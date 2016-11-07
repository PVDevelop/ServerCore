namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public interface IKeyGeneratorService
	{
		/// <summary>
		/// Генерация ключа подтверждения.
		/// </summary>
		string GenerateConfirmationKey();
	}
}
