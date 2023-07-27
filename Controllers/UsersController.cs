using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;
using Sklepix.Models.Users;

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
					FirstName = i.FirstName,
					LastName = i.LastName,
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
				FirstName = userEntity.FirstName,
				LastName = userEntity.LastName,
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
			userVm.Roles = _context.Roles.OrderBy(x => x.Name).ToList();

			return View(userVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserCreateViewModel userVm)
		{
			UserEntity user = new UserEntity()
			{
				UserName = userVm.UserName,
				FirstName = userVm.FirstName,
				LastName = userVm.LastName,
				Email = userVm.Email,
				PhoneNumber = userVm.PhoneNumber,
				CreationTime = DateTime.Now
			};

			await _userManager.CreateAsync(user, userVm.Password);

			string rolesString = userVm.RolesString;

			if(rolesString != null)
			{
				List<string> roles = rolesString.Split(',').ToList();

				foreach(string role in roles)
				{
					if(role != null && role != "")
					{ 
						await _userManager.AddToRoleAsync(user, role);
					}
				}
			}

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(string id)
		{
			UserEntity userEntity = await _userManager.FindByIdAsync(id);
			UserCreateViewModel userVm = new UserCreateViewModel()
			{
				Id = userEntity.Id,
				UserName = userEntity.UserName,
				FirstName = userEntity.FirstName,
				LastName = userEntity.LastName,
				Email = userEntity.Email,
				PhoneNumber = userEntity.PhoneNumber
			};

			userVm.Roles = _context.Roles.OrderBy(x => x.Name).ToList();

			return View(userVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, UserCreateViewModel userVm)
		{
			UserEntity userEntity = await _userManager.FindByIdAsync(id);
			userEntity.UserName = userVm.UserName;
			userEntity.FirstName = userVm.FirstName;
			userEntity.LastName = userVm.LastName;
			userEntity.Email = userVm.Email;
			userEntity.PhoneNumber = userVm.PhoneNumber;

			await _userManager.UpdateAsync(userEntity);

			List<string> allRolesNames = _userManager.GetRolesAsync(userEntity).Result.ToList();

			await _userManager.RemoveFromRolesAsync(userEntity, allRolesNames);

			string rolesString = userVm.RolesString;

			if(rolesString != null)
			{
				List<string> roles = rolesString.Split(',').ToList();

				foreach(string role in roles)
				{
					if(role != null && role != "")
					{
						await _userManager.AddToRoleAsync(userEntity, role);
					}
				}
			}

			return RedirectToAction(nameof(Details), new { id = id });
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
