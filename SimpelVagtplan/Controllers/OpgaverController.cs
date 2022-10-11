using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpelVagtplan.Data;
using SimpelVagtplan.Models;

namespace SimpelVagtplan.Controllers
{
    public class OpgaverController : Controller
    {
        private readonly SimpelVagtplanContext _context;

        public OpgaverController(SimpelVagtplanContext context)
        {
            _context = context;
        }

        // GET: Opgaver
        public async Task<IActionResult> Index()
        {
              return View(await _context.Opgave.ToListAsync());
        }

        // GET: Opgaver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Opgave == null)
            {
                return NotFound();
            }

            var opgave = await _context.Opgave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opgave == null)
            {
                return NotFound();
            }

            return View(opgave);
        }

        // GET: Opgaver/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Opgaver/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,AgeLimit")] Opgave opgave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opgave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opgave);
        }

        // GET: Opgaver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Opgave == null)
            {
                return NotFound();
            }

            var opgave = await _context.Opgave.FindAsync(id);
            if (opgave == null)
            {
                return NotFound();
            }
            return View(opgave);
        }

        // POST: Opgaver/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,AgeLimit")] Opgave opgave)
        {
            if (id != opgave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opgave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpgaveExists(opgave.Id))
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
            return View(opgave);
        }

        // GET: Opgaver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Opgave == null)
            {
                return NotFound();
            }

            var opgave = await _context.Opgave
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opgave == null)
            {
                return NotFound();
            }

            return View(opgave);
        }

        // POST: Opgaver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Opgave == null)
            {
                return Problem("Entity set 'SimpelVagtplanContext.Opgave'  is null.");
            }
            var opgave = await _context.Opgave.FindAsync(id);
            if (opgave != null)
            {
                _context.Opgave.Remove(opgave);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpgaveExists(int id)
        {
          return _context.Opgave.Any(e => e.Id == id);
        }
    }
}
