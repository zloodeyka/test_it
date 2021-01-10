using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Task.DataAccess.Managers;
using Task.DataAccess.Managers.Implementations;
using Task.DataAccess.Services;
using Task.DataAccess.Services.Implementations;

namespace Task.DataAccess
{
	public static class Registrar
	{
		public static void RegisterUsers(this IServiceCollection services)
		{
			RegisterUserContext(services);
			RegisterManagers(services);
		}
		private static void RegisterUserContext(IServiceCollection services)
		{
			services.AddDbContext<Context>(opt =>
											   opt.UseSqlite("Data Source=users.db"));
		}

		private static void RegisterManagers(IServiceCollection services)
		{
			services.AddScoped<IUserManager, UserManager>();
			services.AddScoped<IPasswordService, PasswordService>();
		}
	}
}
