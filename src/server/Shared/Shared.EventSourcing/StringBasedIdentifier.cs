using System;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public abstract class StringBasedIdentifier : IEventSourcingIdentifier
	{
		public string Value { get; private set; }

		protected StringBasedIdentifier()
		{
		}

		protected StringBasedIdentifier(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Not set.", nameof(value));

			Value = value;
		}

		protected bool Equals(StringBasedIdentifier other)
		{
			return string.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((StringBasedIdentifier) obj);
		}

		public override int GetHashCode()
		{
			return (Value != null ? Value.GetHashCode() : 0);
		}

		public string GetIdString()
		{
			return Value;
		}

		public void ParseId(string id)
		{
			if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Not set.", nameof(id));

			Value = id;
		}
	}
}
