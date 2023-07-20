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

		public async Task<IActionResult> Details(string id)
		{
			if(id == null)
			{
				return NotFound();
			}

			UserEntity userEntity = await _userManager.FindByIdAsync(id);

			if(userEntity == null)
			{
				return NotFound();
			}

			UserViewModel userVm = new UserViewModel()
			{
				Id = userEntity.Id,
				UserName = userEntity.UserName,
				Email = userEntity.Email,
				PhoneNumber = userEntity.PhoneNumber,
				CreationTime = userEntity.CreationTime
			};

			return View(userVm);
		}

		public async Task<IActionResult> Delete(string id)
		{
			if(id == null)
			{
				return NotFound();
			}

			UserEntity userEntity = await _userManager.FindByIdAsync(id);

			if(userEntity == null)
			{
				return NotFound();
			}

			UserViewModel userVm = new UserViewModel()
			{
				UserName = userEntity.UserName,
				Email = userEntity.Email
			};

			return View(userVm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			UserEntity userEntity = await _userManager.FindByIdAsync(id);

			if(userEntity != null)
			{
				await _userManager.DeleteAsync(userEntity);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
