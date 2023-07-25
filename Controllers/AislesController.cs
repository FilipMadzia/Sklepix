using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Models;
using Sklepix.Repositories;

namespace Sklepix.Controllers
{
	public class AislesController : Controller
	{
		private readonly AisleRepository _aisleRepository;
		private readonly ShelfRepository _shelfRepository;
		private readonly UserManager<UserEntity> _userManager;

		public AislesController(AisleRepository aisleRepository, ShelfRepository shelfRepository, UserManager<UserEntity> userManager)
		{
			_aisleRepository = aisleRepository;
			_shelfRepository = shelfRepository;
			_userManager = userManager;
		}

		[Authorize(Roles = "Administrator, Alejki - wyswietlanie")]
		public IActionResult Index()
		{
			List<AisleDetailsViewModel> aisleVms = _aisleRepository.GetAisles()
				.Select(i => new AisleDetailsViewModel()
				{
					Id = i.Id,
					Name = i.Name,
					User = i.User != null ? _userManager.FindByIdAsync(i.User.Id).Result : new UserEntity()
				})
				.ToList();

			return View(aisleVms);
		}

		[Authorize(Roles = "Administrator, Alejki - wyswietlanie")]
		public IActionResult Details(int id = -1)
		{
			if(id == -1 || _aisleRepository == null)
			{
				return NotFound();
			}

			AisleEntity aisleEntity = _aisleRepository.GetAisleById(id);

			if(aisleEntity == null)
			{
				return NotFound();
			}

			AisleDetailsViewModel aisleVm = new AisleDetailsViewModel()
			{
				Id = aisleEntity.Id,
				Name = aisleEntity.Name,
				User = aisleEntity.User != null ? _userManager.FindByIdAsync(aisleEntity.User.Id).Result : new UserEntity()
			};

			return View(aisleVm);
		}

		[Authorize(Roles = "Administrator, Alejki - dodawanie")]
		public IActionResult Create()
		{
			return View(new AisleCreateViewModel()
			{
				Users = _userManager.Users.ToList()
			}
			);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AisleCreateViewModel aisleVm)
		{
			AisleEntity aisleEntity = new AisleEntity()
			{
				Id = aisleVm.Id,
				Name = aisleVm.Name,
				User = _userManager.FindByIdAsync(aisleVm.UserId).Result
			};

			if(ModelState.IsValid)
			{
				_aisleRepository.InsertAisle(aisleEntity);
				_aisleRepository.Save();
				return RedirectToAction(nameof(Index));
			}
			return View(aisleVm);
		}

		[Authorize(Roles = "Administrator, Alejki - zmienianie")]
		public IActionResult Edit(int id = -1)
		{
			if(id == -1 || _aisleRepository == null)
			{
				return NotFound();
			}

			AisleEntity aisleEntity = _aisleRepository.GetAisleById(id);

			if(aisleEntity == null)
			{
				return NotFound();
			}

			AisleCreateViewModel aisleVm = new AisleCreateViewModel()
			{
				Id = aisleEntity.Id,
				Name = aisleEntity.Name,
				UserId = aisleEntity.User != null ? aisleEntity.User.Id : "",
				Users = _userManager.Users.ToList()
			};

			return View(aisleVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, AisleCreateViewModel aisleVm)
		{
			AisleEntity aisleEntity = new AisleEntity()
			{
				Id = aisleVm.Id,
				Name = aisleVm.Name,
				User = _userManager.FindByIdAsync(aisleVm.UserId).Result
			};

			if (id != aisleEntity.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_aisleRepository.UpdateAisle(aisleEntity);
					_aisleRepository.Save();
				}
				catch(DbUpdateConcurrencyException)
				{
					if(_aisleRepository.GetAisleById(id) == null)
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Details), new { id = id });
			}
			return View(aisleVm);
		}

		[Authorize(Roles = "Administrator, Alejki - usuwanie")]
		public IActionResult Delete(int id = -1)
		{
			if(id == -1 || _aisleRepository == null)
			{
				return NotFound();
			}

			AisleEntity aisleEntity = _aisleRepository.GetAisleById(id);

			if(aisleEntity == null)
			{
				return NotFound();
			}

			AisleDetailsViewModel aisleVm = new AisleDetailsViewModel()
			{
				Id = aisleEntity.Id,
				Name = aisleEntity.Name
			};

			return View(aisleVm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			AisleEntity aisleEntity = _aisleRepository.GetAisleById(id);

			if(aisleEntity == null)
			{
				return RedirectToAction(nameof(Index));
			}

			if(_shelfRepository.GetShelves().Find(x => x.Aisle.Id == aisleEntity.Id) == null)
			{
				_aisleRepository.DeleteAisle(id);
				_aisleRepository.Save();
			}
			else
			{
				ModelState.AddModelError("Name", "Istnieją półki korzystające z tej alejki. Usuń/zmień wszystkie półki korzystające z tej alejki");
				AisleDetailsViewModel aisleVm = new AisleDetailsViewModel()
				{
					Id = aisleEntity.Id,
					Name = aisleEntity.Name
				};
				return View(aisleVm);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}
