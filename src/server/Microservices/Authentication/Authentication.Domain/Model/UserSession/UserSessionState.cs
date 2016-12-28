namespace PVDevelop.UCoach.Domain.Model.UserSession
{
	/// <summary>
	/// Состояние активности сессии пользователя (синхронизировано с пользовательским состоянием).
	/// </summary>
	public enum UserSessionState
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
