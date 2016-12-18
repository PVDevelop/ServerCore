using System;

namespace PVDevelop.UCoach.Domain.Model
{
	public class ConfirmationKey
	{
		public string Value { get; }

		public ConfirmationKey(string value)
		{
			if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Not set", nameof(value));
			Value = value;
		}

		protected bool Equals(ConfirmationKey other)
		{
			return string.Equals(Value, other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ConfirmationKey) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value;
		}
	}
}
