using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PVDevelop.UCoach.EventStore
{
	public class EventStore : IEventStore
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
				InMemoryEventStream eventStream;
				_streams.TryGetValue(id, out eventStream);
				return eventStream;
			}
		}

		public IEventStream[] GetStreams(Regex streamIdRegex)
		{
			lock (_sync)
			{
				return 
					_streams.
					Where(kvp => streamIdRegex.IsMatch(kvp.Key)).
					Select(kvp => kvp.Value).
					ToArray();
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
