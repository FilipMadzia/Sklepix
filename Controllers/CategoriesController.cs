﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Models;
using Sklepix.Repositories;

namespace Sklepix.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly CategoryRepository _categoryRepository;

		public CategoriesController(CategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IActionResult Index()
		{
			List<CategoryDetailsViewModel> categoryVms = _categoryRepository.GetCategories()
				.Select(i => new CategoryDetailsViewModel()
				{
					Id = i.Id,
					Name = i.Name
				})
				.ToList();

			return View(categoryVms);
		}

		public IActionResult Details(int id = -1)
		{
			if(id == -1 || _categoryRepository == null)
		{
				return NotFound();
			}

			CategoryEntity categoryEntity = _categoryRepository.GetCategoryById(id);

			if(categoryEntity == null)
			{
				return NotFound();
			}

			CategoryDetailsViewModel categoryVm = new CategoryDetailsViewModel()
			{
				Id = categoryEntity.Id,
				Name = categoryEntity.Name
			};

			return View(categoryVm);
		}

		public IActionResult Create()
		{
			return View(new CategoryCreateViewModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(CategoryCreateViewModel categoryVm)
		{
			CategoryEntity categoryEntity = new CategoryEntity()
			{
				Id = categoryVm.Id,
				Name = categoryVm.Name
			};

			if(ModelState.IsValid)
			{
				_categoryRepository.InsertCategory(categoryEntity);
				_categoryRepository.Save();
				return RedirectToAction(nameof(Index));
			}

			return View(categoryVm);
		}

		public IActionResult Edit(int id = -1)
		{
			if(id == -1 || _categoryRepository == null)
			{
				return NotFound();
			}

			CategoryEntity categoryEntity = _categoryRepository.GetCategoryById(id);

			if(categoryEntity == null)
			{
				return NotFound();
			}

			CategoryCreateViewModel categoryVm = new CategoryCreateViewModel()
			{
				Id = categoryEntity.Id,
				Name = categoryEntity.Name
			};

			return View(categoryVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, CategoryCreateViewModel categoryVm)
		{
			CategoryEntity categoryEntity = new CategoryEntity()
			{
				Id = categoryVm.Id,
				Name = categoryVm.Name
			};

			if(id != categoryEntity.Id)
			{
				return NotFound();
			}

			if(ModelState.IsValid)
			{
				try
				{
					_categoryRepository.UpdateCategory(categoryEntity);
					_categoryRepository.Save();
				}
				catch (DbUpdateConcurrencyException)
				{
					if(_categoryRepository.GetCategoryById(id) == null)
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

			return View(categoryVm);
			}

		public IActionResult Delete(int id = -1)
		{
			if(id == -1 || _categoryRepository == null)
			{
				return NotFound();
			}

			CategoryEntity categoryEntity = _categoryRepository.GetCategoryById(id);

			if(categoryEntity == null)
			{
				return NotFound();
			}

			CategoryDetailsViewModel categoryVm = new CategoryDetailsViewModel()
			{
				Id = categoryEntity.Id,
				Name = categoryEntity.Name,
			};

			return View(categoryVm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			if(_categoryRepository == null)
			{
				return Problem("Entity set 'SklepixContext.CategoryEntity'  is null.");
			}

			CategoryEntity categoryEntity = _categoryRepository.GetCategoryById(id);

			if(categoryEntity != null)
			{
				_categoryRepository.DeleteCategory(id);
			}
			
			try
			{
				_categoryRepository.Save();
			}
			catch
			{
				ModelState.AddModelError("Name", "Istnieją produkty korzystające z tej kategorii. Usuń/zmień wszystkie produkty korzystające z tej kategorii");
				CategoryDetailsViewModel modelVm = new CategoryDetailsViewModel()
				{
					Id = categoryEntity.Id,
					Name = categoryEntity.Name
				};
				return View(modelVm);
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
