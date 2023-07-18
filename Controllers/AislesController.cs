using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Models;
using Sklepix.Repositories;

namespace Sklepix.Controllers
{
	[Authorize]
	public class AislesController : Controller
	{
		private readonly AisleRepository _aisleRepository;
		private readonly ShelfRepository _shelfRepository;

		public AislesController(AisleRepository aisleRepository, ShelfRepository shelfRepository)
		{
			_aisleRepository = aisleRepository;
			_shelfRepository = shelfRepository;
		}

		public IActionResult Index()
		{
			List<AisleDetailsViewModel> aisleVms = _aisleRepository.GetAisles()
				.Select(i => new AisleDetailsViewModel()
				{
					Id = i.Id,
					Name = i.Name
				})
				.ToList();

			return View(aisleVms);
		}

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
				Name = aisleEntity.Name
			};

			return View(aisleVm);
		}

		public IActionResult Create()
		{
			return View(new AisleCreateViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(AisleCreateViewModel aisleVm)
		{
			AisleEntity aisleEntity = new AisleEntity()
			{
				Id = aisleVm.Id,
				Name = aisleVm.Name
			};

			if(ModelState.IsValid)
			{
				_aisleRepository.InsertAisle(aisleEntity);
				_aisleRepository.Save();
				return RedirectToAction(nameof(Index));
			}
			return View(aisleVm);
		}

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
				Name = aisleEntity.Name
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
				Name = aisleVm.Name
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
