namespace PVDevelop.UCoach.EventStore
{
	/// <summary>
	/// Хранилище событий.
	/// </summary>
	public interface IEventStore
	{
		/// <summary>
		/// Создать новый поток событий.
		/// </summary>
		/// <param name="id">Идентификатор потока событий.</param>
		/// <returns>Созданный поток событий.</returns>
		IEventStream CreateStream(string id);

		/// <summary>
		/// Вернуть поток по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор искомого потока.</param>
		/// <returns>Поток событий.</returns>
		IEventStream GetStream(string id);
	}
}
