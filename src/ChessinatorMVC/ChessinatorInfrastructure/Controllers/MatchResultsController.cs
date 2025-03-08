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
    public class MatchResultsController : Controller
    {
        private readonly ChessdbContext _context;

        public MatchResultsController(ChessdbContext context)
        {
            _context = context;
        }

        // GET: MatchResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.MatchResults.ToListAsync());
        }

        // GET: MatchResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchResult = await _context.MatchResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchResult == null)
            {
                return NotFound();
            }

            return View(matchResult);
        }

        // GET: MatchResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MatchResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Result,Description")] MatchResult matchResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(matchResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(matchResult);
        }

        // GET: MatchResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchResult = await _context.MatchResults.FindAsync(id);
            if (matchResult == null)
            {
                return NotFound();
            }
            return View(matchResult);
        }

        // POST: MatchResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Result,Description")] MatchResult matchResult)
        {
            if (id != matchResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(matchResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchResultExists(matchResult.Id))
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
            return View(matchResult);
        }

        // GET: MatchResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matchResult = await _context.MatchResults
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matchResult == null)
            {
                return NotFound();
            }

            return View(matchResult);
        }

        // POST: MatchResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matchResult = await _context.MatchResults.FindAsync(id);
            if (matchResult != null)
            {
                _context.MatchResults.Remove(matchResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchResultExists(int id)
        {
            return _context.MatchResults.Any(e => e.Id == id);
        }
    }
}
