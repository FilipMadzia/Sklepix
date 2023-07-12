using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;
using Sklepix.Models;

namespace Sklepix.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SklepixContext _context;

        public CategoriesController(SklepixContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryDetailsViewModel> categoryVms = await _context.CategoryEntity
                .Select(i => new CategoryDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToListAsync();

            return View(categoryVms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.CategoryEntity == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.CategoryEntity
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create(CategoryCreateViewModel categoryVm)
        {
            CategoryEntity categoryEntity = new CategoryEntity()
            {
                Id = categoryVm.Id,
                Name = categoryVm.Name
            };

            if(ModelState.IsValid)
            {
                _context.Add(categoryEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoryVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.CategoryEntity == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.CategoryEntity.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, CategoryCreateViewModel categoryVm)
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
                    _context.Update(categoryEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryEntityExists(categoryEntity.Id))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryEntity == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.CategoryEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoryEntity == null)
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.CategoryEntity == null)
            {
                return Problem("Entity set 'SklepixContext.CategoryEntity'  is null.");
            }
            var categoryEntity = await _context.CategoryEntity.FindAsync(id);
            if(categoryEntity != null)
            {
                _context.CategoryEntity.Remove(categoryEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryEntityExists(int id)
        {
          return (_context.CategoryEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
