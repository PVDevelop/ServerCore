using System.Collections.Concurrent;
using System.Linq;
using PVDevelop.UCoach.Authentication.Domain.Model;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Fake
{
	public class InMemoryConfirmationRepository : IConfirmationRepository
	{
		private readonly ConcurrentDictionary<string, Confirmation> _confirmations = new ConcurrentDictionary<string, Confirmation>();

		public void Replace(Confirmation confirmation)
		{
			_confirmations[confirmation.UserId] = confirmation;
		}

		public Confirmation FindByConfirmation(string key)
		{
			return _confirmations.Values.SingleOrDefault(c => c.Key == key);
		}

		public Confirmation FindByConfirmationByUserId(string userId)
		{
			return _confirmations[userId];
		}
	}
}
