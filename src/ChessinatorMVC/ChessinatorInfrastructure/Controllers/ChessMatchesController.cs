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
    public class ChessMatchesController : Controller
    {
        private readonly ChessdbContext _context;

        public ChessMatchesController(ChessdbContext context)
        {
            _context = context;
        }

        // GET: ChessMatches
        public async Task<IActionResult> Index()
        {
            var chessdbContext = _context.ChessMatches.Include(c => c.BlackPlayer).Include(c => c.MatchResult).Include(c => c.Tournament).Include(c => c.WhitePlayer);
            return View(await chessdbContext.ToListAsync());
        }

        // GET: ChessMatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chessMatch = await _context.ChessMatches
                .Include(c => c.BlackPlayer)
                .Include(c => c.MatchResult)
                .Include(c => c.Tournament)
                .Include(c => c.WhitePlayer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chessMatch == null)
            {
                return NotFound();
            }

            return View(chessMatch);
        }

        // GET: ChessMatches/Create
        public IActionResult Create()
        {
            ViewData["BlackPlayerId"] = new SelectList(_context.Players, "Id", "FirstName");
            ViewData["MatchResultId"] = new SelectList(_context.MatchResults, "Id", "Result");
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name");
            ViewData["WhitePlayerId"] = new SelectList(_context.Players, "Id", "FirstName");
            return View();
        }

        // POST: ChessMatches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WhitePlayerId,BlackPlayerId,RoundNumber,StartTime,EndTime,MatchResultId,Moves,TournamentId")] ChessMatch chessMatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chessMatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlackPlayerId"] = new SelectList(_context.Players, "Id", "FirstName", chessMatch.BlackPlayerId);
            ViewData["MatchResultId"] = new SelectList(_context.MatchResults, "Id", "Result", chessMatch.MatchResultId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", chessMatch.TournamentId);
            ViewData["WhitePlayerId"] = new SelectList(_context.Players, "Id", "FirstName", chessMatch.WhitePlayerId);
            return View(chessMatch);
        }

        // GET: ChessMatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chessMatch = await _context.ChessMatches.FindAsync(id);
            if (chessMatch == null)
            {
                return NotFound();
            }
            ViewData["BlackPlayerId"] = new SelectList(_context.Players, "Id", "FirstName", chessMatch.BlackPlayerId);
            ViewData["MatchResultId"] = new SelectList(_context.MatchResults, "Id", "Result", chessMatch.MatchResultId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", chessMatch.TournamentId);
            ViewData["WhitePlayerId"] = new SelectList(_context.Players, "Id", "FirstName", chessMatch.WhitePlayerId);
            return View(chessMatch);
        }

        // POST: ChessMatches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WhitePlayerId,BlackPlayerId,RoundNumber,StartTime,EndTime,MatchResultId,Moves,TournamentId")] ChessMatch chessMatch)
        {
            if (id != chessMatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chessMatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChessMatchExists(chessMatch.Id))
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
            ViewData["BlackPlayerId"] = new SelectList(_context.Players, "Id", "FirstName", chessMatch.BlackPlayerId);
            ViewData["MatchResultId"] = new SelectList(_context.MatchResults, "Id", "Result", chessMatch.MatchResultId);
            ViewData["TournamentId"] = new SelectList(_context.Tournaments, "Id", "Name", chessMatch.TournamentId);
            ViewData["WhitePlayerId"] = new SelectList(_context.Players, "Id", "FirstName", chessMatch.WhitePlayerId);
            return View(chessMatch);
        }

        // GET: ChessMatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chessMatch = await _context.ChessMatches
                .Include(c => c.BlackPlayer)
                .Include(c => c.MatchResult)
                .Include(c => c.Tournament)
                .Include(c => c.WhitePlayer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chessMatch == null)
            {
                return NotFound();
            }

            return View(chessMatch);
        }

        // POST: ChessMatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chessMatch = await _context.ChessMatches.FindAsync(id);
            if (chessMatch != null)
            {
                _context.ChessMatches.Remove(chessMatch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChessMatchExists(int id)
        {
            return _context.ChessMatches.Any(e => e.Id == id);
        }
    }
}
