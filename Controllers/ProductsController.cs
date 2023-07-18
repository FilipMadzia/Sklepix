using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data.Entities;
using Sklepix.Models;
using Sklepix.Repositories;
using System.Drawing.Printing;

namespace Sklepix.Controllers
{
	[Authorize]
	public class ProductsController : Controller
	{
		private readonly ProductRepository _productRepository;
		private readonly CategoryRepository _categoryRepository;
		private readonly ShelfRepository _shelfRepository;
		private readonly AisleRepository _aisleRepository;

		public ProductsController(ProductRepository productRepository, CategoryRepository categoryRepository, ShelfRepository shelfRepository, AisleRepository aisleRepository)
		{
			_productRepository = productRepository;
			_categoryRepository = categoryRepository;
			_shelfRepository = shelfRepository;
			_aisleRepository = aisleRepository;
		}

		public IActionResult Index()
		{
			List<ProductDetailsViewModel> productVms = _productRepository.GetProducts()
				.Select(i => new ProductDetailsViewModel()
				{
					Id = i.Id,
					Name = i.Name,
					Count = i.Count,
					Price = i.Price,
					Margin = i.Margin + "%",
					TotalPrice = i.Count * i.Price,
					TotalPriceWithMargin = i.Count * i.Price + (decimal)i.Margin / 100 * i.Count * i.Price,
					Category = i.Category.Name,
					ShelfAndAisle = i.Shelf.Number + " | " + i.Shelf.Aisle.Name
				})
				.ToList();

			return View(productVms);
		}

		public IActionResult Details(int id = -1)
		{
			if(id == -1 || _productRepository == null)
			{
				return NotFound();
			}

			ProductEntity productEntity = _productRepository.GetProductById(id);

			if(productEntity == null)
			{
				return NotFound();
			}

			ProductDetailsViewModel productVm = new ProductDetailsViewModel()
			{
				Id = productEntity.Id,
				Name = productEntity.Name,
				Count = productEntity.Count,
				Price = productEntity.Price,
				Margin = productEntity.Margin + "%",
				TotalPrice = productEntity.Count * productEntity.Price,
				TotalPriceWithMargin = productEntity.Count * productEntity.Price + (decimal)productEntity.Margin / 100 * productEntity.Count * productEntity.Price,
				Category = productEntity.Category.Name,
				ShelfAndAisle = productEntity.Shelf.Number + " | " + productEntity.Shelf.Aisle.Name
			};

			return View(productVm);
		}

		public IActionResult Create()
		{
			ProductCreateViewModel productVm = new ProductCreateViewModel()
			{
				Categories = _categoryRepository.GetCategories(),
				Shelves = _shelfRepository.GetShelves(),
				Aisles = _aisleRepository.GetAisles()
			};
			
			return View(productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductCreateViewModel productVm)
		{
			ProductEntity productEntity = new ProductEntity()
			{
				Name = productVm.Name,
				Price = productVm.Price,
				Count = productVm.Count,
				Margin = productVm.Margin,
				Category = _categoryRepository.GetCategoryById(productVm.CategoryId),
				Shelf = _shelfRepository.GetShelfById(productVm.ShelfId)
			};

			if(productEntity.Category == null)
			{
				throw new Exception();
			}

			if(ModelState.IsValid)
			{
				_productRepository.InsertProduct(productEntity);
				_productRepository.Save();
				return RedirectToAction(nameof(Index));
			}

			productVm.Categories = _categoryRepository.GetCategories();
			productVm.Shelves = _shelfRepository.GetShelves();
			productVm.Aisles = _aisleRepository.GetAisles();

			return View(productVm);
		}

		public IActionResult Edit(int id = -1)
		{
			if(id == -1 || _productRepository == null)
			{
				return NotFound();
			}

			ProductEntity productEntity = _productRepository.GetProductById(id);

			if(productEntity == null)
			{
				return NotFound();
			}

			ProductCreateViewModel productVm = new ProductCreateViewModel()
			{
				Id = productEntity.Id,
				Name = productEntity.Name,
				Count = productEntity.Count,
				Price = productEntity.Price,
				Margin = productEntity.Margin,
				Categories = _categoryRepository.GetCategories(),
				CategoryId = productEntity.Category.Id,
				Shelves = _shelfRepository.GetShelves(),
				ShelfId = productEntity.Shelf.Id,
				Aisles = _aisleRepository.GetAisles()
			};

			return View(productVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id, ProductCreateViewModel productVm)
		{
			ProductEntity productEntity = new ProductEntity()
			{
				Id = productVm.Id,
				Name = productVm.Name,
				Count = productVm.Count,
				Price = productVm.Price,
				Margin = productVm.Margin,
				Category = _categoryRepository.GetCategoryById(productVm.CategoryId),
				Shelf = _shelfRepository.GetShelfById(productVm.ShelfId)
			};

			if(id != productEntity.Id)
			{
				return NotFound();
			}

			if(ModelState.IsValid)
			{
				try
				{
					_productRepository.UpdateProduct(productEntity);
					_productRepository.Save();
				}
				catch(DbUpdateConcurrencyException)
				{
					if(_productRepository.GetProductById(id) == null)
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

			productVm.Categories = _categoryRepository.GetCategories();
			productVm.Shelves = _shelfRepository.GetShelves();
			productVm.Aisles = _aisleRepository.GetAisles();

			return View(productVm);
		}

		public IActionResult Delete(int id = -1)
		{
			if(id == -1 || _productRepository == null)
			{
				return NotFound();
			}

			ProductEntity productEntity = _productRepository.GetProductById(id);

			if(productEntity == null)
			{
				return NotFound();
			}

			ProductDetailsViewModel productVm = new ProductDetailsViewModel()
			{
				Id = productEntity.Id,
				Name = productEntity.Name,
				Count = productEntity.Count,
				Price = productEntity.Price,
				Margin = productEntity.Margin + "%",
				TotalPrice = productEntity.Count * productEntity.Price,
				TotalPriceWithMargin = productEntity.Count * productEntity.Price + (decimal)productEntity.Margin / 100 * productEntity.Count * productEntity.Price,
				Category = productEntity.Category.Name,
				ShelfAndAisle = productEntity.Shelf.Number + " | " + productEntity.Shelf.Aisle.Name
			};

			return View(productVm);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			if(_productRepository == null)
			{
				return Problem("Entity set 'SklepixContext.Product'  is null.");
			}
			ProductEntity productEntity = _productRepository.GetProductById(id);

			if(productEntity != null)
			{
				_productRepository.DeleteProduct(id);
			}
			
			_productRepository.Save();
			return RedirectToAction(nameof(Index));
		}
	}
}
