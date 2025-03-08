using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessinatorDomain.Model;

namespace ChessinatorInfrastructure.Controllers
{
    public class TimeControlsController : Controller
    {
        private readonly ChessdbContext _context;

        public TimeControlsController(ChessdbContext context)
        {
            _context = context;
        }

        // GET: TimeControls
        public async Task<IActionResult> Index()
        {
            var chessdbContext = _context.TimeControls.Include(t => t.Type);
            return View(await chessdbContext.ToListAsync());
        }

        // GET: TimeControls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeControl = await _context.TimeControls
                .Include(t => t.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeControl == null)
            {
                return NotFound();
            }

            return View(timeControl);
        }

        // GET: TimeControls/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.TimeControlTypes, "Id", "Name");
            return View();
        }

        // POST: TimeControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BaseMinutes,IncSeconds,TypeId")] TimeControl timeControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.TimeControlTypes, "Id", "Name", timeControl.TypeId);
            return View(timeControl);
        }

        // GET: TimeControls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeControl = await _context.TimeControls.FindAsync(id);
            if (timeControl == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.TimeControlTypes, "Id", "Name", timeControl.TypeId);
            return View(timeControl);
        }

        // POST: TimeControls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BaseMinutes,IncSeconds,TypeId")] TimeControl timeControl)
        {
            if (id != timeControl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeControlExists(timeControl.Id))
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
            ViewData["TypeId"] = new SelectList(_context.TimeControlTypes, "Id", "Name", timeControl.TypeId);
            return View(timeControl);
        }

        // GET: TimeControls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeControl = await _context.TimeControls
                .Include(t => t.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeControl == null)
            {
                return NotFound();
            }

            return View(timeControl);
        }

        // POST: TimeControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeControl = await _context.TimeControls.FindAsync(id);
            if (timeControl != null)
            {
                _context.TimeControls.Remove(timeControl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeControlExists(int id)
        {
            return _context.TimeControls.Any(e => e.Id == id);
        }
    }
}
