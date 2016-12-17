using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Port
{
	public interface IConfirmationKeyGenerator
	{
		/// <summary>
		/// Генерирует ключ подтверждения создания пользователя.
		/// </summary>
		/// <returns>Ключ подтверждения.</returns>
		ConfirmationKey Generate();
	}
}
