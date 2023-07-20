using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklepix.Data.Entities;
using Sklepix.Models;

namespace Sklepix.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class UsersController : Controller
	{
		UserManager<UserEntity> _userManager;

		public UsersController(UserManager<UserEntity> userManager)
		{
			_userManager = userManager;
		}
		public IActionResult Index()
		{
			List<UserViewModel> userVms = _userManager.Users
				.Select(i => new UserViewModel()
				{
					Id = i.Id,
					UserName = i.UserName,
					Email = i.Email,
					PhoneNumber = i.PhoneNumber
				})
				.ToList();

			return View(userVms);
		}
	}
}
