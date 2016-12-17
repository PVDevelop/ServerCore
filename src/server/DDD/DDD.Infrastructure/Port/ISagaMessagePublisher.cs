using PVDevelop.UCoach.Saga;

namespace PVDevelop.UCoach.Infrastructure.Port
{
	public interface ISagaMessagePublisher
	{
		/// <summary>
		/// Направляет сообщение адресату.
		/// </summary>
		/// <param name="message">Направляемое сообщение.</param>
		void Publish(ISagaMessage message);
	}
}
