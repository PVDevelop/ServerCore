using System;
using PVDevelop.UCoach.Domain.Model;
using PVDevelop.UCoach.Domain.Service;

namespace PVDevelop.UCoach.Authentication.Infrastructure
{
	public class ConfirmationKeyGenerator : IConfirmationKeyGenerator
	{
		public ConfirmationKey Generate()
		{
			return new ConfirmationKey(Guid.NewGuid().ToString());
		}
	}
}
