using System;
using System.Collections.Generic;
using Task.DataAccess.Entities;

namespace Task.DataAccess.Managers
{
	public interface IUserManager
	{
		IEnumerable<UserData> GetList();

		UserData GetById(long userId);

		void Add(UserData model);

		void Update(long userId, UserData model);

		void Delete(long userId);
	}
}
