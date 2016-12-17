using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public interface IEventStream
	{
		/// <summary>
		/// Идентификатор стрима.
		/// </summary>
		string StreamId { get; }

		/// <summary>
		/// Сохранить последовательность событий.
		/// </summary>
		/// <param name="events">ПО=оследовательность сохраняемых событий.</param>
		void SaveEvents(IReadOnlyCollection<object> events);

		/// <summary>
		/// Возвращает последовательность событий.
		/// </summary>
		/// <param name="startVersion">Номер начального события.</param>
		/// <param name="endVersion">Номер последнего события.</param>
		/// <returns>Последовательность событий.</returns>
		EventsData GetEvents(int startVersion, int endVersion);
	}
}
