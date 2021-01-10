using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Task.Api.Entities;
using Task.DataAccess.Entities;
using Task.DataAccess.Managers;

namespace Task.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		public UsersController(IUserManager userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IEnumerable<UserModel> Get()
		{
			return _userManager.GetList()
				.Select(x=> new UserModel
				{
					Id = x.Id,
					Name = x.Name,
					Email = x.Email
				});
		}

		[HttpGet("{id}")]
		public UserEditModel Get(int id)
		{
			var user = _userManager.GetById(id);

			if (user == null)
			{
				throw new InvalidOperationException("User not found");
			}

			return new UserEditModel
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email
			};
		}

		[HttpPost]
		public void Post([FromBody] UserEditModel user)
		{
			_userManager.Add(new UserData
			{
				Id = user.Id,
				Name = user.Name?.Trim(),
				Email = user.Email?.Trim(),
				Password = user.Password?.Trim()
			});
		}

		// PUT api/<UsersController>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] UserEditModel user)
		{
			_userManager.Update(id, new UserData
			{
				Id = user.Id,
				Name = user.Name?.Trim(),
				Email = user.Email?.Trim(),
				Password = user.Password?.Trim()
			});
		}

		// DELETE api/<UsersController>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			_userManager.Delete(id);
		}

		private readonly IUserManager _userManager;
	}
}
