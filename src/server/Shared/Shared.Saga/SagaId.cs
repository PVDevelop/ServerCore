using System;

namespace PVDevelop.UCoach.Saga
{
	public class SagaId
	{
		public Guid Value { get; }

		public SagaId(Guid value)
		{
			Value = value;
		}

		protected bool Equals(SagaId other)
		{
			return Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((SagaId) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
