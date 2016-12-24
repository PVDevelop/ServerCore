using System;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class ProcessId
	{
		public Guid Value { get; }

		public ProcessId(Guid value)
		{
			Value = value;
		}

		protected bool Equals(ProcessId other)
		{
			return Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ProcessId) obj);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}
