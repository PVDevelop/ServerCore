namespace PVDevelop.UCoach.Saga
{
	/// <summary>
	/// Состояние процессы выполнения саги.
	/// </summary>
	public enum SagaStatus
	{
		/// <summary>
		/// Новая сага.
		/// </summary>
		New = 0,

		/// <summary>
		/// В процессе выполнения.
		/// </summary>
		Progress = 1,

		/// <summary>
		/// Выполнение завершено успешно.
		/// </summary>
		Succeeded = 2
	}
}
