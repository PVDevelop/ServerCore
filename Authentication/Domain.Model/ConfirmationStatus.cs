namespace PVDevelop.UCoach.Authentication.Domain.Model
{
	/// <summary>
	/// Статус жизненого цикла для пользователя.
	/// Создался пользователь (Unconfirmed) -> подтвердили (Confirmed)
	/// </summary>
	public enum ConfirmationStatus
	{
		Unconfirmed = 0,
		Confirmed = 1
	}
}

