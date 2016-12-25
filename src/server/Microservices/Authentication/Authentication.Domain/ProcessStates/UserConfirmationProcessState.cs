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
		/// Пользователь подтвержден. Конец процесса.
		/// </summary>
		UserConfirmed = 2
	}
}
