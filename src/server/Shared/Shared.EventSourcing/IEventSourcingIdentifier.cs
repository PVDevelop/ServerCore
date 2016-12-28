namespace PVDevelop.UCoach.Shared.EventSourcing
{
	public interface IEventSourcingIdentifier
	{
		string GetIdString();

		void ParseId(string id);
	}
}
