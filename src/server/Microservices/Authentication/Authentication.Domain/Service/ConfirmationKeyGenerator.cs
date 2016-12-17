using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Port;

namespace PVDevelop.UCoach.Domain.Service
{
	public class ConfirmationKeyGenerator : IConfirmationKeyGenerator
	{
		public ConfirmationKey Generate()
		{
			return new ConfirmationKey(Guid.NewGuid().ToString());
		}
	}
}
