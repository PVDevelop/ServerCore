using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public class EventsData
	{
		public int LatestVersion { get; }
		public IReadOnlyCollection<object> Events { get; }

		public EventsData(int latestVersion, IReadOnlyCollection<object> events)
		{
			if (events == null) throw new ArgumentNullException(nameof(events));
			LatestVersion = latestVersion;
			Events = events;
		}
	}
}
