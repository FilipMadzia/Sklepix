using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return _context.ProductEntity != null ? 
                View(await _context.ProductEntity.OrderByDescending(s => s.Category.Name).ToListAsync()) :
                Problem("Entity set 'SklepixContext.Product'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductEntity == null)
            {
                return NotFound();
            }

            var product = await _context.ProductEntity
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ProductCreateViewModel model = new ProductCreateViewModel();
            model.Categories = categories;
            model.Shelves = shelves;
            model.Aisles = aisles;
            
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel productVm)
        {
            ProductEntity product = new ProductEntity()
            {
                Name = productVm.Name,
                Price = productVm.Price,
                Category = categories.Find(x => x.Id == productVm.CategoryId),
                Shelf = shelves.Find(x => x.Id == productVm.ShelfId)
            };

            if(product.Category == null)
            {
                throw new Exception();
            }

            if(ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productVm);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductEntity == null)
            {
                return NotFound();
            }

            var product = await _context.ProductEntity.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ProductCreateViewModel productVm = new ProductCreateViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Categories = categories,
                CategoryId = product.Category != null ? product.Category.Id : throw new Exception(),
                Shelves = shelves,
                ShelfId = product.Shelf != null ? product.Shelf.Id : throw new Exception(),
                Aisles = aisles
            };
            return View(productVm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductCreateViewModel productVm)
        {
            ProductEntity product = new ProductEntity()
            {
                Id = productVm.Id,
                Name = productVm.Name,
                Price = productVm.Price,
                Category = categories.Find(x => x.Id == productVm.CategoryId),
                Shelf = shelves.Find(x => x.Id == productVm.ShelfId)
            };

            if(product.Category == null)
            {
                throw new Exception();
            }
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductEntity == null)
            {
                return NotFound();
            }

            var product = await _context.ProductEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductEntity == null)
            {
                return Problem("Entity set 'SklepixContext.Product'  is null.");
            }
            var product = await _context.ProductEntity.FindAsync(id);
            if (product != null)
            {
                _context.ProductEntity.Remove(product);
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
