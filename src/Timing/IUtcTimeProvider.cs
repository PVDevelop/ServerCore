using System;

namespace PVDevelop.UCoach.Timing
{
	public interface IUtcTimeProvider
	{
		DateTime UtcNow { get; }
	}
}
