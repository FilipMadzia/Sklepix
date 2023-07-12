using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;
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

        public async Task<IActionResult> Index()
        {
            List<ShelfDetailsViewModel> shelfVms = await _context.ShelfEntity
                .Select(i => new ShelfDetailsViewModel()
                {
                    Id = i.Id,
                    Number = i.Number,
                    Aisle = i.Aisle.Name
                })
                .ToListAsync();

            return View(shelfVms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.ShelfEntity == null)
            {
                return NotFound();
            }

            var shelfEntity = await _context.ShelfEntity
                .Include(m => m.Aisle)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            ShelfCreateViewModel shelfVm = new ShelfCreateViewModel();
            shelfVm.Aisles = _context.AisleEntity != null ? _context.AisleEntity.ToList() : new List<AisleEntity>();
            
            return View(shelfVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ShelfCreateViewModel shelfVm)
        {
            ShelfEntity shelfEntity = new ShelfEntity()
            {
                Number = shelfVm.Number,
                Aisle = aisles.Find(x => x.Id == shelfVm.AisleId)
            };

            if(ModelState.IsValid)
            {
                _context.Add(shelfEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shelfVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.ShelfEntity == null)
            {
                return NotFound();
            }

            var shelfEntity = await _context.ShelfEntity.FindAsync(id);
            if(shelfEntity == null)
            {
                return NotFound();
            }

            ShelfCreateViewModel shelfVm = new ShelfCreateViewModel()
            {
                Id = shelfEntity.Id,
                Number = shelfEntity.Number,
                Aisles = aisles,
                AisleId = shelfEntity.Aisle.Id
            };

            return View(shelfVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ShelfCreateViewModel shelfVm)
        {
            ShelfEntity shelfEntity = new ShelfEntity()
            {
                Id = shelfVm.Id,
                Number = shelfVm.Number,
                Aisle = aisles.Find(x => x.Id == shelfVm.AisleId)
            };

            if(id != shelfEntity.Id)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
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

            return View(shelfVm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.ShelfEntity == null)
            {
                return NotFound();
            }

            var shelfEntity = await _context.ShelfEntity
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.ShelfEntity == null)
            {
                return Problem("Entity set 'SklepixContext.ShelfEntity'  is null.");
            }
            var shelfEntity = await _context.ShelfEntity.FindAsync(id);
            if(shelfEntity != null)
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
