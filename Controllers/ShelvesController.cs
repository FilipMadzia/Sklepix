using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Models;

namespace Sklepix.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly SklepixContext _context;
        private readonly List<AisleEntity> aisles;

        public ShelvesController(SklepixContext context)
        {
            _context = context;
            aisles = _context.AisleEntity!= null ? _context.AisleEntity.ToList() : new List<AisleEntity>();
        }

        // GET: ShelfEntities
        public async Task<IActionResult> Index()
        {
              return _context.ShelfEntity != null ? 
                          View(await _context.ShelfEntity.OrderByDescending(s => s.Aisle.Name).ToListAsync()) :
                          Problem("Entity set 'SklepixContext.ShelfEntity'  is null.");
        }

        // GET: ShelfEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ShelfEntity == null)
            {
                return NotFound();
            }

            var shelfEntity = await _context.ShelfEntity
                .Include(m => m.Aisle)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelfEntity == null)
            {
                return NotFound();
            }

            return View(shelfEntity);
        }

        // GET: ShelfEntities/Create
        public IActionResult Create()
        {
            ShelfViewModel model = new ShelfViewModel();
            model.Aisles = _context.AisleEntity != null ? _context.AisleEntity.ToList() : new List<AisleEntity>();
            
            return View(model);
        }

        // POST: ShelfEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelfViewModel shelfVm)
        {
            ShelfEntity shelf = new ShelfEntity()
            {
                Number = shelfVm.Number,
                Aisle = aisles.Find(x => x.Id == shelfVm.AisleId)
            };

            if (ModelState.IsValid)
            {
                _context.Add(shelf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shelf);
        }

        // GET: ShelfEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ShelfEntity == null)
            {
                return NotFound();
            }

            var shelfEntity = await _context.ShelfEntity.FindAsync(id);
            if (shelfEntity == null)
            {
                return NotFound();
            }
            return View(shelfEntity);
        }

        // POST: ShelfEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number")] ShelfEntity shelfEntity)
        {
            if (id != shelfEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shelfEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelfEntityExists(shelfEntity.Id))
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
            return View(shelfEntity);
        }

        // GET: ShelfEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ShelfEntity == null)
            {
                return NotFound();
            }

            var shelfEntity = await _context.ShelfEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelfEntity == null)
            {
                return NotFound();
            }

            return View(shelfEntity);
        }

        // POST: ShelfEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ShelfEntity == null)
            {
                return Problem("Entity set 'SklepixContext.ShelfEntity'  is null.");
            }
            var shelfEntity = await _context.ShelfEntity.FindAsync(id);
            if (shelfEntity != null)
            {
                _context.ShelfEntity.Remove(shelfEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelfEntityExists(int id)
        {
          return (_context.ShelfEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
