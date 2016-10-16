using System;

namespace PVDevelop.UCoach.Timing
{
	public class UtcTimeProvider : IUtcTimeProvider
	{
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
