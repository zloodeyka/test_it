using System;
using System.Collections.Generic;
using System.Text;

namespace Task.DataAccess.Managers
{
	public class User
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public byte[] Password { get; set; }

		public byte[] Salt { get; set; }
	}
}
