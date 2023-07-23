using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklepix.Data;
using Sklepix.Data.Entities;
using Sklepix.Models;

namespace Sklepix.Controllers
{
	[Authorize(Roles = "Administrator")]
	public class UsersController : Controller
	{
		SklepixContext _context;
		UserManager<UserEntity> _userManager;

		public UsersController(SklepixContext context, UserManager<UserEntity> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			List<UserDetailsViewModel> userVms = _userManager.Users
				.Select(i => new UserDetailsViewModel()
				{
					Id = i.Id,
					UserName = i.UserName,
					Email = i.Email,
					PhoneNumber = i.PhoneNumber,
					Roles = _userManager.GetRolesAsync(i).Result.ToList()
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

			UserDetailsViewModel userVm = new UserDetailsViewModel()
			{
				Id = userEntity.Id,
				UserName = userEntity.UserName,
				Email = userEntity.Email,
				PhoneNumber = userEntity.PhoneNumber,
				CreationTime = userEntity.CreationTime,
				Roles = _userManager.GetRolesAsync(userEntity).Result.ToList()
			};

			return View(userVm);
		}

		public IActionResult Create()
		{
			UserCreateViewModel userVm = new UserCreateViewModel();
			userVm.Roles = _context.Roles.ToList();

			return View(userVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserCreateViewModel userVm)
		{
			UserEntity user = new UserEntity()
			{
				UserName = userVm.UserName,
				Email = userVm.Email,
				PhoneNumber = userVm.PhoneNumber,
				CreationTime = DateTime.Now
			};

			await _userManager.CreateAsync(user, userVm.Password);

			return RedirectToAction(nameof(Index));
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

			UserDetailsViewModel userVm = new UserDetailsViewModel()
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
