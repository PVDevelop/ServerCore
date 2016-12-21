using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Authentication.Infrastructure.Adapter
{
	public class ConfirmationKeyGenerator : IConfirmationKeyGenerator
	{
		public ConfirmationKey Generate()
		{
			return new ConfirmationKey(Guid.NewGuid().ToString());
		}
	}
}
