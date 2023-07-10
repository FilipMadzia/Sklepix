using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Models;

namespace Sklepix.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SklepixContext _context;

        public ProductsController(SklepixContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.ProductEntity != null ? 
                          View(await _context.ProductEntity.ToListAsync()) :
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
            ProductViewModel model = new ProductViewModel();
            model.Categories = _context.CategoryEntity != null ? _context.CategoryEntity.ToList() : new List<CategoryEntity>();
            
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productVm)
        {
            List<CategoryEntity>? categories = _context.CategoryEntity.ToList();
            ProductEntity product = new ProductEntity()
            {
                Name = productVm.Name,
                Price = productVm.Price,
                Category = categories.Find(x => x.Id == productVm.CategoryId)
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
            ProductViewModel productVm = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Categories = _context.CategoryEntity.ToList(),
                CategoryId = product.Category.Id
            };
            return View(productVm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel productVm)
        {
            List<CategoryEntity>? categories = _context.CategoryEntity.ToList();
            ProductEntity product = new ProductEntity()
            {
                Name = productVm.Name,
                Price = productVm.Price,
                Category = categories.Find(x => x.Id == productVm.CategoryId)
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
