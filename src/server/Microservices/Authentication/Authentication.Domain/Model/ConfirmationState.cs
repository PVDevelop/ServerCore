namespace PVDevelop.UCoach.Domain.Model
{
	/// <summary>
	/// Состояние подтверждения.
	/// </summary>
	public enum ConfirmationState
	{
		/// <summary>
		/// Подтверждение создано, но еще не отправлено.
		/// </summary>
		New = 0,

		/// <summary>
		/// В ожидании подтверждения.
		/// </summary>
		Pending = 1,

		/// <summary>
		/// Подтвержден.
		/// </summary>
		Confirmed = 2
	}
}
