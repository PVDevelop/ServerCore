using System.Text.RegularExpressions;

namespace PVDevelop.UCoach.Infrastructure.Adapter
{
	/// <summary>
	/// Фильтр обозреваемых потоков EventStream'a.
	/// </summary>
	public class EventStoreFilter
	{
		/// <summary>
		/// Регулярное вырадение, определяющее список запрещенных потоков.
		/// Если не пусто, то ничего не запрещено.
		/// </summary>
		public string BlackListStreamRegex { get; }

		public EventStoreFilter(string blackListStreamRegex)
		{
			BlackListStreamRegex = blackListStreamRegex;
		}

		public bool Filtrate(string streamId)
		{
			if (string.IsNullOrWhiteSpace(BlackListStreamRegex)) return true;

			return !Regex.IsMatch(streamId, BlackListStreamRegex);
		}
	}
}
