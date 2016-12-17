using System;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserId
	{
		public Guid Value { get; }

		public UserId(Guid value)
		{
			if (value == default(Guid)) throw new ArgumentException("Not set", nameof(value));
			Value = value;
		}

		protected bool Equals(UserId other)
		{
			return Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((UserId)obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}
