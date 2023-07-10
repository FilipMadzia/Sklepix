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
    public class AislesController : Controller
    {
        private readonly SklepixContext _context;

        public AislesController(SklepixContext context)
        {
            _context = context;
        }

        // GET: AisleEntities
        public async Task<IActionResult> Index()
        {
              return _context.AisleEntity != null ? 
                          View(await _context.AisleEntity.ToListAsync()) :
                          Problem("Entity set 'SklepixContext.AisleEntity'  is null.");
        }

        // GET: AisleEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AisleEntity == null)
            {
                return NotFound();
            }

            var aisleEntity = await _context.AisleEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aisleEntity == null)
            {
                return NotFound();
            }

            return View(aisleEntity);
        }

        // GET: AisleEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AisleEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AisleEntity aisleEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aisleEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aisleEntity);
        }

        // GET: AisleEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AisleEntity == null)
            {
                return NotFound();
            }

            var aisleEntity = await _context.AisleEntity.FindAsync(id);
            if (aisleEntity == null)
            {
                return NotFound();
            }
            return View(aisleEntity);
        }

        // POST: AisleEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AisleEntity aisleEntity)
        {
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
            return View(aisleEntity);
        }

        // GET: AisleEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AisleEntity == null)
            {
                return NotFound();
            }

            var aisleEntity = await _context.AisleEntity
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aisleEntity == null)
            {
                return NotFound();
            }

            return View(aisleEntity);
        }

        // POST: AisleEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AisleEntity == null)
            {
                return Problem("Entity set 'SklepixContext.AisleEntity'  is null.");
            }
            var aisleEntity = await _context.AisleEntity.FindAsync(id);
            if (aisleEntity != null)
            {
                _context.AisleEntity.Remove(aisleEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AisleEntityExists(int id)
        {
          return (_context.AisleEntity?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
