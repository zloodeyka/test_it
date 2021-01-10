using Microsoft.EntityFrameworkCore;
using Task.DataAccess.Managers;

namespace Task.DataAccess
{
	internal class Context : DbContext
	{
		public Context(DbContextOptions<Context> options)
	: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
	}
}
