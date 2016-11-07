namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Состояние пользователя. Создался пользователь (New) -> подтвердили (Confirmed).
	/// </summary>
	public enum UserState
	{
		/// <summary>
		/// Новый пользователь.
		/// </summary>
		New = 0,

		/// <summary>
		/// Пользователь подтверден.
		/// </summary>
		Confirmed = 1
	}
}

