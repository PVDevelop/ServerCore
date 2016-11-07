namespace PVDevelop.UCoach.AuthenticationApp.Domain.Model
{
	/// <summary>
	/// Состояние подтверждения.
	/// </summary>
	public enum ConfirmationState
	{
		/// <summary>
		/// В ожидании подтверждения.
		/// </summary>
		Pending = 0,

		/// <summary>
		/// Подтвержден.
		/// </summary>
		Confirmed = 1
	}
}
