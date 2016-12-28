using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain.Model
{
	public class ConfirmationKey : StringBasedIdentifier
	{
		public ConfirmationKey()
		{
		}

		public ConfirmationKey(string value) : base(value)
		{
		}
	}
}