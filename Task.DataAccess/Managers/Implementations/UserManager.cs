using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Task.DataAccess.Entities;
using Task.DataAccess.Services;

namespace Task.DataAccess.Managers.Implementations
{
	internal class UserManager : IUserManager
	{
		public UserManager(Context context,
			IPasswordService passworddService)
		{
			_context = context;
			_passworddService = passworddService;
		}

		public IEnumerable<UserData> GetList()
		{
			return _context.Users.Select(x=> new UserData
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email
			});
		}

		public UserData GetById(long userId)
		{
			var entity = _context.Users.SingleOrDefault(x => x.Id == userId);

			if (entity == null)
			{
				throw new InvalidOperationException("User not found");
			}

			return new UserData
			{
				Id = entity.Id,
				Name = entity.Name,
				Email = entity.Email
			};
		}

		public void Add(UserData model)
		{
			ValidateUser(model);

			var entity = new User
			{
				Name = model.Name,
				Email = model.Email
			};

			entity.Salt = _passworddService.GenerateSalt();
			entity.Password = _passworddService.GetPasswordHash(model.Password, entity.Salt);

			_context.Users.Add(entity);
			_context.SaveChanges();
		}

		public void Update(long userId, UserData model)
		{
			model.Id = userId;
			ValidateUser(model);

			var entity = _context.Users
				.Single(x => x.Id == userId);

			entity.Name = model.Name;
			entity.Email = model.Email;

			entity.Salt = _passworddService.GenerateSalt();
			entity.Password = _passworddService.GetPasswordHash(model.Password, entity.Salt);

			_context.Users.Update(entity);
			_context.SaveChanges();
			
		}

		public void Delete(long userId)
		{
			var user = _context.Users.Single(x => x.Id == userId);

			_context.Users.Remove(user);
			_context.SaveChanges();
		}

		private void ValidateUser(UserData model)
		{
			if (string.IsNullOrEmpty(model.Name))
			{
				throw new InvalidOperationException("Username is empty");
			}

			if (_context.Users.Any(x=> x.Name == model.Name && x.Id!=model.Id))
			{
				throw new InvalidOperationException("Username have already been used");
			}

			if (string.IsNullOrEmpty(model.Email))
			{
				throw new InvalidOperationException("Email is empty");
			}

			if (_context.Users.Any(x => x.Email == model.Email && x.Id != model.Id))
			{
				throw new InvalidOperationException("Email have already been used");
			}

			if (!Regex.IsMatch(model.Email, EmailPattern, EmailRegexOptions, MatchTimeout))
			{
				throw new InvalidOperationException("Ivalid email format");
			}

			if (string.IsNullOrEmpty(model.Password))
			{
				throw new InvalidOperationException("Password is empty");
			}
		}

		private readonly Context _context;
		private readonly IPasswordService _passworddService;
		private const string EmailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
		private const RegexOptions EmailRegexOptions =
			RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant;

		private static readonly TimeSpan MatchTimeout = TimeSpan.FromSeconds(10);
	}
}
