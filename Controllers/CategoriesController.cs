using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;

namespace Sklepix.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly SklepixContext _context;

        public CategoriesController(SklepixContext context)
        {
            _context = context;
        }

        // GET: CategoryEntities
        public async Task<IActionResult> Index()
        {
              return _context.CategoryEntity != null ? 
                          View(await _context.CategoryEntity.ToListAsync()) :
                          Problem("Entity set 'SklepixContext.CategoryEntity'  is null.");
        }

        // GET: CategoryEntities/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(categoryEntity);
        }

        // GET: CategoryEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryEntity categoryEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryEntity);
        }

        // GET: CategoryEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryEntity == null)
            {
                return NotFound();
            }

            var categoryEntity = await _context.CategoryEntity.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }
            return View(categoryEntity);
        }

        // POST: CategoryEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryEntity categoryEntity)
        {
            if (id != categoryEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
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
            return View(categoryEntity);
        }

        // GET: CategoryEntities/Delete/5
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

            return View(categoryEntity);
        }

        // POST: CategoryEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryEntity == null)
            {
                return Problem("Entity set 'SklepixContext.CategoryEntity'  is null.");
            }
            var categoryEntity = await _context.CategoryEntity.FindAsync(id);
            if (categoryEntity != null)
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
