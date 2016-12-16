using System;
using System.Collections.Generic;

namespace PVDevelop.UCoach.Saga
{
	public class Saga
	{
		private readonly List<ISagaMessage> _sagaMessages = new List<ISagaMessage>();

		public Guid Id { get; }

		public Saga(Guid id)
		{
			Id = id;
		}

		public void AddMessage(ISagaMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));
			_sagaMessages.Add(message);
		}
	}
}
