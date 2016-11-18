using PVDevelop.UCoach.AuthenticationApp.Domain.Model;

namespace PVDevelop.UCoach.AuthenticationApp.Infrastructure
{
	public interface IConfirmationRepository
	{
		/// <summary>
		/// Добавить новое подтверждение.
		/// </summary>
		void Insert(Confirmation confirmation);

		/// <summary>
		/// Найти подтверждение по ключу подтверждения. Еслин не находит, возвращает null.
		/// </summary>
		Confirmation FindByConfirmationKey(string key);

		/// <summary>
		/// Обновить имеющееся подтверждение.
		/// </summary>
		void Update(Confirmation confirmation);
	}
}
