using System;
using PVDevelop.UCoach.Domain.Service.Message;

namespace PVDevelop.UCoach.Domain.Service
{
	public class UserCreationSaga
	{
		private readonly CreateUser _createUser;

		public UserCreationSaga(CreateUser createUser)
		{
			if (createUser == null) throw new ArgumentNullException(nameof(createUser));
			_createUser = createUser;
		}
	}
}
