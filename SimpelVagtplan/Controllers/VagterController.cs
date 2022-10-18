using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SimpelVagtplan.Data;
using SimpelVagtplan.Models;

namespace SimpelVagtplan.Controllers
{
    public class VagterController : Controller
    {
        private readonly SimpelVagtplanContext _context;

        public VagterController(SimpelVagtplanContext context)
        {
            _context = context;
        }

        // GET: Vagter
        public async Task<IActionResult> Index()
        {
            ViewBag.medarbejdere = _context.Medarbejder.ToList();
            ViewBag.opgaver = _context.Opgave.ToList();
            return View(await _context.Vagt.ToListAsync());
        }

        // GET: Vagter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vagt == null)
            {
                return NotFound();
            }

            var vagt = await _context.Vagt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vagt == null)
            {
                return NotFound();
            }

            return View(vagt);
        }

        // GET: Vagter/Create
        public IActionResult Create()
        { 

            ViewBag.medarbejdere = _context.Medarbejder.ToList();
            return View();
        }

        // POST: Vagter/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Medarbejder,StartTime,EndTime")] Vagt vagt, int medarbejderId)
        {
            ViewBag.medarbejdere = _context.Medarbejder.ToList();
            var m = await _context.Medarbejder.FirstAsync(m => m.Id == medarbejderId);
            vagt.Medarbejder = m;
            _context.Add(vagt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vagter/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vagt == null)
            {
                return NotFound();
            }

            var vagt = await _context.Vagt.FindAsync(id);
            if (vagt == null)
            {
                return NotFound();
            }
            return View(vagt);
        }

        // POST: Vagter/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MedarbejderId,StartTime,EndTime")] Vagt vagt)
        {
            if (id != vagt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vagt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VagtExists(vagt.Id))
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
            return View(vagt);
        }

        
        public async Task<IActionResult> AddOpgave(int id)
        {

            ViewBag.medarbejdere = _context.Medarbejder.ToList();
            ViewBag.opgaver = _context.Opgave.ToList().Where(o => o.vagt == null);
            return View(await _context.Vagt.FindAsync(id));
        }

        public async Task<IActionResult> AddOpgaveToVagt(int vagtId, int opgaveId)
        {
            var opgave = await _context.Opgave.FindAsync(opgaveId);
            var vagt = await _context.Vagt.FindAsync(vagtId);
            opgave.vagt = vagt;
            try
            {
                _context.Update(opgave);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveOpgave(int id)
        {
            var opgave = await _context.Opgave.FindAsync(id);
            
            
            opgave.vagt = null;
            _context.Update(opgave);
            

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }
        // GET: Vagter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vagt == null)
            {
                return NotFound();
            }

            var vagt = await _context.Vagt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vagt == null)
            {
                return NotFound();
            }

            return View(vagt);
        }

        // POST: Vagter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vagt == null)
            {
                return Problem("Entity set 'SimpelVagtplanContext.Vagt'  is null.");
            }
            var vagt = await _context.Vagt.FindAsync(id);
            if (vagt != null)
            {
                _context.Vagt.Remove(vagt);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VagtExists(int id)
        {
          return _context.Vagt.Any(e => e.Id == id);
        }
    }
}
