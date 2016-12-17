using System;
using System.Collections.Generic;
using System.Linq;

namespace PVDevelop.UCoach.EventStore
{
	public class InMemoryEventStore : IEventStore
	{
		private readonly object _sync = new object();

		private readonly Dictionary<string, InMemoryEventStream> _streams =
			new Dictionary<string, InMemoryEventStream>();

		public IEventStream GetOrCreateStream(string id)
		{
			lock (_sync)
			{
				InMemoryEventStream stream;
				if (!_streams.TryGetValue(id, out stream))
				{
					stream = new InMemoryEventStream(id);
					_streams.Add(id, stream);
				}

				return stream;
			}
		}

		public IEventStream GetStream(string id)
		{
			lock (_sync)
			{
				return _streams[id];
			}
		}

		public IReadOnlyCollection<IEventStream> GetAllStreams()
		{
			lock (_sync)
			{
				return _streams.Values.ToArray();
			}
		}
	}
}
