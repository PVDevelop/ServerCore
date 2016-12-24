using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Shared.ProcessManagement.Events
{
	public class ProcessCreated : IProcessSourcingEvent
	{
		public List<ProcessStateDescription> Descriptions { get; }

		public ProcessCreated(List<ProcessStateDescription> descriptions)
		{
			if (descriptions == null) throw new ArgumentNullException(nameof(descriptions));

			Descriptions = descriptions;
		}
	}
}
