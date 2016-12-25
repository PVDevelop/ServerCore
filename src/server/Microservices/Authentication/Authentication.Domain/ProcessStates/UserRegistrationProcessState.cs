namespace PVDevelop.UCoach.Domain.ProcessStates
{
	/// <summary>
	/// Состояние процесса регистрации пользователя.
	/// </summary>
	public enum UserRegistrationProcessState
	{
		/// <summary>
		/// Создался запрос на регистрацию пользователя.
		/// </summary>
		RegisterUserRequested = 0,

		/// <summary>
		/// Создался пользователь.
		/// </summary>
		UserCreated = 1,

		/// <summary>
		/// Создалось подтверждение, ожидаем, когда пользователь подтвердит регистрацию. Конец процесса.
		/// </summary>
		ConfirmationCreated = 2
	}
}
