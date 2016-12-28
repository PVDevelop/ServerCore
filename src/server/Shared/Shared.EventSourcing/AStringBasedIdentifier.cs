using System;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public abstract class AStringBasedIdentifier
	{
		public string Value { get; }

		protected AStringBasedIdentifier(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Not set.", nameof(value));

			Value = value;
		}

		protected bool Equals(AStringBasedIdentifier other)
		{
			return string.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((AStringBasedIdentifier) obj);
		}

		public override int GetHashCode()
		{
			return (Value != null ? Value.GetHashCode() : 0);
		}
	}
}
