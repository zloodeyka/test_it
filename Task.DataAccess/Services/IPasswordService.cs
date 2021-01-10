using System;
using System.Collections.Generic;
using System.Text;

namespace Task.DataAccess.Services
{
	interface IPasswordService
	{
		byte[] GetPasswordHash(string password, byte[] salt);

		byte[] GenerateSalt();
	}
}
