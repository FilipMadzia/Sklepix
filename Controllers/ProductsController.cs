using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;
using Sklepix.Models;

namespace Sklepix.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SklepixContext _context;
        private readonly List<CategoryEntity> categories;
        private readonly List<ShelfEntity> shelves;
        private readonly List<AisleEntity> aisles;

        public ProductsController(SklepixContext context)
        {
            _context = context;
            categories = _context.CategoryEntity != null ? _context.CategoryEntity.ToList() : new List<CategoryEntity>();
            shelves = _context.ShelfEntity!= null ? _context.ShelfEntity.OrderByDescending(s => s.Aisle.Name).ToList() : new List<ShelfEntity>();
            aisles = _context.AisleEntity!= null ? _context.AisleEntity.ToList() : new List<AisleEntity>();
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDetailsViewModel> productVms = await _context.ProductEntity
                .OrderByDescending(s => s.Category.Name)
                .Select(i => new ProductDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    Category = i.Category.Name,
                    ShelfAndAisle = i.Shelf.Number + " | " + i.Shelf.Aisle.Name
                })
                .ToListAsync();

            return View(productVms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.ProductEntity == null)
            {
                return NotFound();
            }

            var productEntity = await _context.ProductEntity
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if(productEntity == null)
            {
                return NotFound();
            }

            ProductDetailsViewModel productVm = new ProductDetailsViewModel()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price,
                Category = productEntity.Category.Name,
                ShelfAndAisle = productEntity.Shelf.Number + " | " + productEntity.Shelf.Aisle.Name
            };

            return View(productVm);
        }

        public IActionResult Create()
        {
            ProductCreateViewModel productVm = new ProductCreateViewModel()
            {
                Categories = categories,
                Shelves = shelves,
                Aisles = aisles
            };
            
            return View(productVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel productVm)
        {
            ProductEntity productEntity = new ProductEntity()
            {
                Name = productVm.Name,
                Price = productVm.Price,
                Category = categories.Find(x => x.Id == productVm.CategoryId),
                Shelf = shelves.Find(x => x.Id == productVm.ShelfId)
            };

            if(productEntity.Category == null)
            {
                throw new Exception();
            }

            if(ModelState.IsValid)
            {
                _context.Add(productEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            productVm.Categories = categories;
            productVm.Shelves = shelves;
            productVm.Aisles = aisles;

            return View(productVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.ProductEntity == null)
            {
                return NotFound();
            }

            var productEntity = await _context.ProductEntity.FindAsync(id);
            if(productEntity == null)
            {
                return NotFound();
            }

            ProductCreateViewModel productVm = new ProductCreateViewModel()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price,
                Categories = categories,
                CategoryId = productEntity.Category.Id,
                Shelves = shelves,
                ShelfId = productEntity.Shelf.Id,
                Aisles = aisles
            };

            return View(productVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel productVm)
        {
            ProductEntity productEntity = new ProductEntity()
            {
                Id = productVm.Id,
                Name = productVm.Name,
                Price = productVm.Price,
                Category = categories.Find(x => x.Id == productVm.CategoryId),
                Shelf = shelves.Find(x => x.Id == productVm.ShelfId)
            };

            if(id != productEntity.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(productEntity);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!ProductExists(productEntity.Id))
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

            productVm.Categories = categories;
            productVm.Shelves = shelves;
            productVm.Aisles = aisles;

            return View(productVm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.ProductEntity == null)
            {
                return NotFound();
            }

            var productEntity = await _context.ProductEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if(productEntity == null)
            {
                return NotFound();
            }

            ProductDetailsViewModel productVm = new ProductDetailsViewModel()
            {
                Id = productEntity.Id,
                Name = productEntity.Name,
                Price = productEntity.Price,
                Category = productEntity.Category.Name,
                ShelfAndAisle = productEntity.Shelf.Number + " | " + productEntity.Shelf.Aisle.Name
            };

            return View(productVm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.ProductEntity == null)
            {
                return Problem("Entity set 'SklepixContext.Product'  is null.");
            }
            var productEntity = await _context.ProductEntity.FindAsync(id);
            if(productEntity != null)
            {
                _context.ProductEntity.Remove(productEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.ProductEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
