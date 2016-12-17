﻿using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	/// <summary>
	/// Хранилище событий.
	/// </summary>
	public interface IEventStore
	{
		/// <summary>
		/// Вернуть имеющийся или создать новый поток событий.
		/// </summary>
		/// <param name="id">Идентификатор потока событий.</param>
		/// <returns>Поток событий.</returns>
		IEventStream GetOrCreateStream(string id);

		/// <summary>
		/// Вернуть поток по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор искомого потока.</param>
		/// <returns>Поток событий.</returns>
		IEventStream GetStream(string id);

		/// <summary>
		/// Возваращает все доступные стримы.
		/// </summary>
		IReadOnlyCollection<IEventStream> GetAllStreams();
	}
}
