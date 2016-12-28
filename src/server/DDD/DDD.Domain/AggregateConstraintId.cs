using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain
{
	public class AggregateConstraintId : IEventSourcingIdentifier
	{
		public string Key { get; private set; }
		public string Value { get; private set; }

		public AggregateConstraintId()
		{
		}

		public AggregateConstraintId(string key, string value)
		{
			if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Not set.", nameof(key));
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Not set.", nameof(value));

			Key = key;
			Value = value;
		}

		protected bool Equals(AggregateConstraintId other)
		{
			return string.Equals(Key, other.Key) && string.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((AggregateConstraintId) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
			}
		}

		public string GetIdString()
		{
			return $"{Key}#{Value}";
		}

		public void ParseId(string id)
		{
			if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Not set.", nameof(id));

			var substrings = id.Split('#');
			Key = substrings[0];
			Value = substrings[1];
		}
	}
}