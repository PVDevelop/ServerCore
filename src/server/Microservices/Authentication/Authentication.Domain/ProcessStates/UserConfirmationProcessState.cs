namespace PVDevelop.UCoach.Domain.ProcessStates
{
	public enum UserConfirmationProcessState
	{
		/// <summary>
		/// Пользователь создал запрос на подтерждение.
		/// </summary>
		ConfirmationRequested = 0,

		/// <summary>
		/// Подтверждение прянито.
		/// </summary>
		ConfirmationApproved = 1,

		/// <summary>
		/// Пользователь подтвержден.
		/// </summary>
		UserConfirmed = 2,

		/// <summary>
		/// Сессия пользователя начата. Конец процесса.
		/// </summary>
		SessionStarted = 3
	}
}
