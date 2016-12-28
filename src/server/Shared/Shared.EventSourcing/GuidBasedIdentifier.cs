using System;

namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public abstract class GuidBasedIdentifier : IEventSourcingIdentifier
	{
		public Guid Value { get; private set; }

		protected GuidBasedIdentifier() { }

		protected GuidBasedIdentifier(Guid value)
		{
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
			return Equals((StringBasedIdentifier)obj);
		}

		public override int GetHashCode()
		{
			return (Value != null ? Value.GetHashCode() : 0);
		}

		public string GetIdString()
		{
			return Value.ToString();
		}

		public void ParseId(string id)
		{
			if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Not set.", nameof(id));

			Value = Guid.Parse(id);
		}
	}
}
