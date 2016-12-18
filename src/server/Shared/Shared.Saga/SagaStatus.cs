namespace PVDevelop.UCoach.Saga
{
	/// <summary>
	/// Состояние процессы выполнения саги.
	/// </summary>
	public enum SagaStatus
	{
		/// <summary>
		/// В процессе выполнения.
		/// </summary>
		Progress = 0,

		/// <summary>
		/// Выполнение завершено успешно.
		/// </summary>
		Succeeded = 1
	}
}
