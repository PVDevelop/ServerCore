using System;

namespace PVDevelop.UCoach.Domain
{
	public class AggregateConstraintId
	{
		public string Key { get; }
		public string Value { get; }

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
	}
}