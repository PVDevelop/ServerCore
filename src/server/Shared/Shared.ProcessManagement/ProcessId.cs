using System;
using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Shared.ProcessManagement
{
	public class ProcessId : GuidBasedIdentifier
	{
		public ProcessId()
		{
		}

		public ProcessId(Guid value) : base(value)
		{
		}
	}
}