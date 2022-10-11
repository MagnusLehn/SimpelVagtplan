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
    public class MedarbejdereController : Controller
    {
        private readonly SimpelVagtplanContext _context;

        public MedarbejdereController(SimpelVagtplanContext context)
        {
            _context = context;
        }

        // GET: Medarbejders
        public async Task<IActionResult> Index()
        {
              return View(await _context.Medarbejder.ToListAsync());
        }

        // GET: Medarbejders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medarbejder == null)
            {
                return NotFound();
            }

            var medarbejder = await _context.Medarbejder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medarbejder == null)
            {
                return NotFound();
            }

            return View(medarbejder);
        }

        // GET: Medarbejders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medarbejders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age")] Medarbejder medarbejder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medarbejder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medarbejder);
        }

        // GET: Medarbejders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medarbejder == null)
            {
                return NotFound();
            }

            var medarbejder = await _context.Medarbejder.FindAsync(id);
            if (medarbejder == null)
            {
                return NotFound();
            }
            return View(medarbejder);
        }

        // POST: Medarbejders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age")] Medarbejder medarbejder)
        {
            if (id != medarbejder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medarbejder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedarbejderExists(medarbejder.Id))
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
            return View(medarbejder);
        }

        // GET: Medarbejders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medarbejder == null)
            {
                return NotFound();
            }

            var medarbejder = await _context.Medarbejder
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medarbejder == null)
            {
                return NotFound();
            }

            return View(medarbejder);
        }

        // POST: Medarbejders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medarbejder == null)
            {
                return Problem("Entity set 'SimpelVagtplanContext.Medarbejder'  is null.");
            }
            var medarbejder = await _context.Medarbejder.FindAsync(id);
            if (medarbejder != null)
            {
                _context.Medarbejder.Remove(medarbejder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedarbejderExists(int id)
        {
          return _context.Medarbejder.Any(e => e.Id == id);
        }
    }
}
