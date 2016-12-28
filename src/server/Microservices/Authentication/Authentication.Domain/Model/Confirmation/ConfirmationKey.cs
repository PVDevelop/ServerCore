using PVDevelop.UCoach.Shared.EventSourcing;

namespace PVDevelop.UCoach.Domain.Model.Confirmation
{
	public class ConfirmationKey : AStringBasedIdentifier
	{
		public ConfirmationKey(string value) : base(value)
		{
		}
	}
}