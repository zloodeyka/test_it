using System;
using System.Collections.Generic;
using System.Text;

namespace Task.DataAccess.Entities
{
	public class UserData
	{
		public long Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}
