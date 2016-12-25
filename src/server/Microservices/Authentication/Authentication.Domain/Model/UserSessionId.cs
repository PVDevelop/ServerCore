using System;

namespace PVDevelop.UCoach.Domain.Model
{
	public class UserSessionId
	{
		public Guid Value { get; }

		public UserSessionId(Guid value)
		{
			Value = value;
		}

		protected bool Equals(UserSessionId other)
		{
			return Value.Equals(other.Value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((UserSessionId) obj);
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
