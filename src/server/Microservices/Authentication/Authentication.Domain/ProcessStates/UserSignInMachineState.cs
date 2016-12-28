namespace PVDevelop.UCoach.Domain.ProcessStates
{
	public enum UserSignInMachineState
	{
		/// <summary>
		/// Запрос на вход в систему.
		/// </summary>
		SignInRequested = 0,

		/// <summary>
		/// Вход разрешен.
		/// </summary>
		SignInApproved = 1,

		/// <summary>
		/// Токен создан.
		/// </summary>
		TokenGenerated = 2
	}
}
