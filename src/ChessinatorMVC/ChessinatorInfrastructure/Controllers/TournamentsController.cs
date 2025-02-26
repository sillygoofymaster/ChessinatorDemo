using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessinatorDomain.Model;
using ChessinatorInfrastructure;

namespace ChessinatorInfrastructure.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly ChessdbContext _context;

        public TournamentsController(ChessdbContext context)
        {
            _context = context;
        }

        // GET: Tournaments
        public async Task<IActionResult> Index()
        {
            var chessdbContext = _context.Tournaments.Include(t => t.Organizer).Include(t => t.TimeControl).Include(t => t.Venue);
            return View(await chessdbContext.ToListAsync());
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.TimeControl)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Email");
            ViewData["TimeControlId"] = new SelectList(_context.TimeControls, "Id", "Type");
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Adress");
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Type,Name,StartTime,EndTime,IsOnline,IsOpen,PlayerLimit,RoundCount,Link,VenueId,TimeControlId,OrganizerId,Id")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Email", tournament.OrganizerId);
            ViewData["TimeControlId"] = new SelectList(_context.TimeControls, "Id", "Type", tournament.TimeControlId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Adress", tournament.VenueId);
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Email", tournament.OrganizerId);
            ViewData["TimeControlId"] = new SelectList(_context.TimeControls, "Id", "Type", tournament.TimeControlId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Adress", tournament.VenueId);
            return View(tournament);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Type,Name,StartTime,EndTime,IsOnline,IsOpen,PlayerLimit,RoundCount,Link,VenueId,TimeControlId,OrganizerId,Id")] Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(tournament.Id))
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
            ViewData["OrganizerId"] = new SelectList(_context.Organizers, "Id", "Email", tournament.OrganizerId);
            ViewData["TimeControlId"] = new SelectList(_context.TimeControls, "Id", "Type", tournament.TimeControlId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Adress", tournament.VenueId);
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.TimeControl)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament != null)
            {
                _context.Tournaments.Remove(tournament);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}
