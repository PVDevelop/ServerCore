namespace PVDevelop.UCoach.Domain.Model
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
		Approved = 1
	}
}
