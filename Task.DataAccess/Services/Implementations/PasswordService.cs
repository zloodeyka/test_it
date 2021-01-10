using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Task.DataAccess.Services.Implementations
{
	internal class PasswordService : IPasswordService
	{
		public PasswordService()
		{
			_sha256 = SHA256.Create();
		}

		public byte[] GetPasswordHash(string password, byte[] salt)
		{
			var bytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();

			return _sha256.ComputeHash(bytes);
		}

		public byte[] GenerateSalt()
		{
			return Guid.NewGuid().ToByteArray();
		}

		private readonly SHA256 _sha256;
	}
}
