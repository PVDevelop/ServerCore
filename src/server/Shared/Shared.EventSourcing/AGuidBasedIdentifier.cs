using System;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public abstract class AGuidBasedIdentifier
	{
		public Guid Value { get; }

		protected AGuidBasedIdentifier(Guid value)
		{
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
