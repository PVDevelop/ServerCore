using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public interface IEventStream
	{
		/// <summary>
		/// Сохранить последовательность событий.
		/// </summary>
		/// <param name="events">ПО=оследовательность сохраняемых событий.</param>
		void SaveEvents(IReadOnlyCollection<object> events);

		/// <summary>
		/// Возвращает последовательность событий.
		/// </summary>
		/// <returns>Последовательность событий.</returns>
		IEnumerable<object> GetEvents();
	}
}
