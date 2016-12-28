using System.Collections.Generic;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public interface IEventSourcingHelper<TId, in TEvent, out TEventSourcing>
		where TEventSourcing : AEventSourcing<TId, TEvent>
	{
		string GetStreamName();

		TId ParseId(string id);

		string GetStringId(TId id);

		TEventSourcing CreateEventSourcing(TId id, int version, IEnumerable<TEvent> events);
	}
}
