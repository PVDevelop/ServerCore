using PVDevelop.UCoach.Domain.Model;

namespace PVDevelop.UCoach.Domain.Service
{
	public interface IConfirmationSender
	{
		void Send(ConfirmationKey confirmationKey);
	}
}
