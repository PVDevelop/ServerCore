using System;
using PVDevelop.UCoach.Domain.Messages;
using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Domain.Service
{
	public class UserSagaMessagesConsumer : ISagaMessageConsumer
	{
		public void Consume(ISagaMessage message)
		{
			if (message == null) throw new ArgumentNullException(nameof(message));
			When((dynamic)message);
		}

		private void When(CreateUserMessage createUserMessage)
		{
			throw new NotImplementedException();
		}

		private void When(object message)
		{
			throw new InvalidOperationException($"Unknown message {message}");
		}
	}
}
