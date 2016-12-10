using System.Collections.Generic;

namespace PVDevelop.UCoach.EventStore
{
	public class InMemoryEventStore : IEventStore
	{
		private readonly Dictionary<string, InMemoryEventStream> _streams = 
			new Dictionary<string, InMemoryEventStream>();

		public IEventStream CreateStream(string id)
		{
			var stream = new InMemoryEventStream();
			_streams.Add(id, stream);
			return stream;
		}

		public IEventStream GetStream(string id)
		{
			return _streams[id];
		}
	}
}
