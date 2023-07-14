using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Models;
using Sklepix.Repositories;

namespace Sklepix.Controllers
{
	public class ShelvesController : Controller
	{
		private readonly ShelfRepository _shelfRepository;
		private readonly AisleRepository _aisleRepository;

		public ShelvesController(ShelfRepository shelfRepository, AisleRepository aisleRepository)
		{
			_shelfRepository = shelfRepository;
			_aisleRepository = aisleRepository;
		}

		public IActionResult Index()
		{
			List<ShelfDetailsViewModel> shelfVms = _shelfRepository.GetShelves()
				.Select(i => new ShelfDetailsViewModel()
				{
					Id = i.Id,
					Number = i.Number,
					Aisle = i.Aisle.Name
				})
				.ToList();

			return View(shelfVms);
		}

		public IActionResult Details(int id = -1)
		{
			if(id == -1 || _shelfRepository == null)
			{
				return NotFound();
			}

			ShelfEntity shelfEntity = _shelfRepository.GetShelfById(id);

			if(shelfEntity == null)
			{
				return NotFound();
			}

			ShelfDetailsViewModel shelfVm = new ShelfDetailsViewModel()
			{
				Id = shelfEntity.Id,
				Number = shelfEntity.Number,
				Aisle = shelfEntity.Aisle.Name
			};

			return View(shelfVm);
		}

		public IActionResult Create()
		{
			ShelfCreateViewModel shelfVm = new ShelfCreateViewModel()
			{
				Aisles = _aisleRepository.GetAisles()
			};
			
			return View(shelfVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ShelfCreateViewModel shelfVm)
		{
			ShelfEntity shelfEntity = new ShelfEntity()
			{
				Number = shelfVm.Number,
				Aisle = _aisleRepository.GetAisleById(shelfVm.AisleId)
			};

			if(ModelState.IsValid)
			{
				_shelfRepository.InsertShelf(shelfEntity);
				_shelfRepository.Save();
				return RedirectToAction(nameof(Index));
			}
			return View(shelfVm);
		}

		public IActionResult Edit(int id = -1)
		{
			if(id == -1 || _shelfRepository == null)
			{
				return NotFound();
			}

			ShelfEntity shelfEntity = _shelfRepository.GetShelfById(id);

			if(shelfEntity == null)
			{
				return NotFound();
			}

			ShelfCreateViewModel shelfVm = new ShelfCreateViewModel()
			{
				Id = shelfEntity.Id,
				Number = shelfEntity.Number,
				Aisles = _aisleRepository.GetAisles(),
				AisleId = shelfEntity.Aisle.Id
			};

			return View(shelfVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, ShelfCreateViewModel shelfVm)
		{
			ShelfEntity shelfEntity = new ShelfEntity()
			{
				Id = shelfVm.Id,
				Number = shelfVm.Number,
				Aisle = _aisleRepository.GetAisleById(shelfVm.AisleId)
			};

			if(id != shelfEntity.Id)
			{
				return NotFound();
			}

			if(ModelState.IsValid)
			{
				try
				{
					_shelfRepository.UpdateShelf(shelfEntity);
					_shelfRepository.Save();
				}
				catch (DbUpdateConcurrencyException)
				{
					if(_shelfRepository.GetShelfById(id) == null)
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}

			return View(shelfVm);
		}

		public IActionResult Delete(int id = -1)
		{
			if(id == -1 || _shelfRepository == null)
			{
				return NotFound();
			}

			ShelfEntity shelfEntity = _shelfRepository.GetShelfById(id);

			if(shelfEntity == null)
			{
				return NotFound();
			}

			ShelfDetailsViewModel shelfVm = new ShelfDetailsViewModel()
			{
				Id = shelfEntity.Id,
				Number = shelfEntity.Number,
				Aisle = shelfEntity.Aisle.Name
			};

			return View(shelfVm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			if(_shelfRepository == null)
			{
				return Problem("Entity set 'SklepixContext.ShelfEntity'  is null.");
			}

			ShelfEntity shelfEntity = _shelfRepository.GetShelfById(id);

			if(shelfEntity != null)
			{
				_shelfRepository.DeleteShelf(id);
			}
			
			_shelfRepository.Save();
			return RedirectToAction(nameof(Index));
		}
	}
}
