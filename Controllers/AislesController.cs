using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklepix.Data;
using Sklepix.Data.Entities;
using Sklepix.Models;

namespace Sklepix.Controllers
{
    public class AislesController : Controller
    {
        private readonly SklepixContext _context;

        public AislesController(SklepixContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<AisleDetailsViewModel> aisleVms = await _context.AisleEntity
                .Select(i => new AisleDetailsViewModel()
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToListAsync();

            return View(aisleVms);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.AisleEntity == null)
            {
                return NotFound();
            }

            var aisleEntity = await _context.AisleEntity
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create(AisleCreateViewModel aisleVm)
        {
            AisleEntity aisleEntity = new AisleEntity()
            {
                Id = aisleVm.Id,
                Name = aisleVm.Name
            };

            if (ModelState.IsValid)
            {
                _context.Add(aisleEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aisleVm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || _context.AisleEntity == null)
            {
                return NotFound();
            }

            var aisleEntity = await _context.AisleEntity.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, AisleCreateViewModel aisleVm)
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
                    _context.Update(aisleEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AisleEntityExists(aisleEntity.Id))
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
            return View(aisleVm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.AisleEntity == null)
            {
                return NotFound();
            }

            var aisleEntity = await _context.AisleEntity
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.AisleEntity == null)
            {
                return Problem("Entity set 'SklepixContext.AisleEntity'  is null.");
            }
            var aisleEntity = await _context.AisleEntity.FindAsync(id);
            if(aisleEntity != null)
            {
                _context.AisleEntity.Remove(aisleEntity);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ModelState.AddModelError("Name", "Istnieją półki korzystające z tej alejki. Usuń/zmień wszystkie półki korzystające z tej alejki");
                AisleDetailsViewModel aisleVm= new AisleDetailsViewModel()
                {
                    Id = aisleEntity.Id,
                    Name = aisleEntity.Name
                };
                return View(aisleVm);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AisleEntityExists(int id)
        {
          return (_context.AisleEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
