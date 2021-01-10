using Microsoft.EntityFrameworkCore;
using Task.DataAccess.Managers;

namespace Task.DataAccess
{
	internal class Context : DbContext
	{

		public DbSet<User> Users { get; set; }

		public Context(DbContextOptions<Context> options): base(options){}

	}
}
