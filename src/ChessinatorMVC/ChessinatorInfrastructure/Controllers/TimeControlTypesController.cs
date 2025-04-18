using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessinatorDomain.Model;
using Microsoft.AspNetCore.Authorization;

namespace ChessinatorInfrastructure.Controllers
{
    public class TimeControlTypesController : Controller
    {
        private readonly ChessdbContext _context;

        public TimeControlTypesController(ChessdbContext context)
        {
            _context = context;
        }

        // GET: TimeControlTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TimeControlTypes.ToListAsync());
        }

        // GET: TimeControlTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeControlType = await _context.TimeControlTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeControlType == null)
            {
                return NotFound();
            }

            return View(timeControlType);
        }

        // GET: TimeControlTypes/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeControlTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TimeControlType timeControlType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeControlType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeControlType);
        }

        // GET: TimeControlTypes/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeControlType = await _context.TimeControlTypes.FindAsync(id);
            if (timeControlType == null)
            {
                return NotFound();
            }
            return View(timeControlType);
        }

        // POST: TimeControlTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TimeControlType timeControlType)
        {
            if (id != timeControlType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeControlType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeControlTypeExists(timeControlType.Id))
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
            return View(timeControlType);
        }

        // GET: TimeControlTypes/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeControlType = await _context.TimeControlTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeControlType == null)
            {
                return NotFound();
            }

            return View(timeControlType);
        }

        // POST: TimeControlTypes/Delete/5

        [Authorize(Roles = "admin")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeControlType = await _context.TimeControlTypes.FindAsync(id);
            if (timeControlType != null)
            {
                _context.TimeControlTypes.Remove(timeControlType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeControlTypeExists(int id)
        {
            return _context.TimeControlTypes.Any(e => e.Id == id);
        }
    }
}
