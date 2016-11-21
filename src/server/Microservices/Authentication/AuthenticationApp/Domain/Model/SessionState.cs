namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Состояние активности сессии пользователя (синхронизировано с пользовательским состоянием).
	/// </summary>
	public enum SessionState
	{
		/// <summary>
		/// Сессия неактивна (пользователь не в сети).
		/// </summary>
		Inactive = 0,

		/// <summary>
		/// Сессия активна (пользователь в сети).
		/// </summary>
		Active = 1
	}
}
