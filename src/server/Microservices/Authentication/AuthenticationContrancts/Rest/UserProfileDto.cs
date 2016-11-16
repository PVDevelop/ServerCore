using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PVDevelop.UCoach.AuthenticationContrancts.Rest
{
	public class UserProfileDto
	{
		public string Email { get; }

		public UserProfileDto(string email)
		{
			if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Not set", nameof(email));

			Email = email;
		}
	}
}
