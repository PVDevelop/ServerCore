namespace PVDevelop.UCoach.Domain.Model
{
	public enum UserState
	{
		/// <summary>
		/// Пользователь в ожидании подтверждения создания.
		/// </summary>
		WaitingForCreationConfirm = 0,

		/// <summary>
		/// Пользователь в сети.
		/// </summary>
		SignedIn = 1,

		/// <summary>
		/// Пользователь не в сети.
		/// </summary>
		SignedOut = 2
	}
}
