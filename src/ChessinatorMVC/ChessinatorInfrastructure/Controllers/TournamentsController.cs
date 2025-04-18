using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessinatorDomain.Model;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ChessinatorInfrastructure.ViewModel;

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
            if (User.IsInRole("player"))
            {
                var playerId = int.Parse(User.FindFirst("PlayerId")!.Value);

                var joinedTournamentIds = _context.PlayerTournaments
                    .Where(pt => pt.PlayerId == playerId)
                    .Select(pt => pt.TournamentId)
                    .ToList();

                ViewBag.JoinedTournamentIds = joinedTournamentIds;
            }
            var chessdbContext = _context.Tournaments.Include(t => t.Organizer).Include(t => t.TimeControl).Include(t => t.TournamentType).Include(t => t.Venue);
            return View(await chessdbContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "player")]
        public async Task<IActionResult> Participate(int tournamentId, string tournamentName)
        {
            var playerIdClaim = User.FindFirst("PlayerId")?.Value;
            if (string.IsNullOrEmpty(playerIdClaim))
            {
                return Unauthorized("PlayerId not found in user claims.");
            }

            if (!int.TryParse(playerIdClaim, out int playerId))
            {
                return BadRequest("Невалідний ID.");
            }

            bool alreadyParticipating = _context.PlayerTournaments
                .Any(pt => pt.TournamentId == tournamentId && pt.PlayerId == playerId);

            if (alreadyParticipating)
            {
                TempData["Error"] = $"Ви вже подалися на участь в івенті {tournamentName}.";
                return RedirectToAction("Index");
            }

            var participation = new PlayerTournament
            {
                PlayerId = playerId,
                TournamentId = tournamentId,
                Score = 0 
            };

            _context.PlayerTournaments.Add(participation);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Ви успішно подалися на участь в івенті {tournamentName}.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "player")]
        public async Task<IActionResult> Disparticipate(int tournamentId, string tournamentName)
        {
            var playerIdClaim = User.FindFirst("PlayerId")?.Value;

            if (!int.TryParse(playerIdClaim, out int playerId))
            {
                return BadRequest("Невалідний ID.");
            }

            var participation = await _context.PlayerTournaments
                .FirstOrDefaultAsync(pt => pt.PlayerId == playerId && pt.TournamentId == tournamentId);

            if (participation == null)
            {
                return NotFound("Participation not found.");
            }

            _context.PlayerTournaments.Remove(participation);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Ви відмовилися від участі в івенті {tournamentName}.";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> PlayerDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.PlayerTournaments)!
                .ThenInclude(o=>o.Player)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            var viewModel = new TournamentPlayerDetailsViewModel
            {
                Tournament = tournament,
                PlayerTournaments = tournament.PlayerTournaments
            };

            return View(viewModel);
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*            var tournament = await _context.Tournaments
                            .Include(t => t.Organizer)
                            .Include(t => t.TimeControl)
                            .Include(t => t.TournamentType)
                            .Include(t => t.Venue)
                            .FirstOrDefaultAsync(m => m.Id == id);*/
            var tournament = await _context.Tournaments
                .Include(t => t.Rounds)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }
            var viewModel = new TournamentDetailsViewModel
            {
                Tournament = tournament,
               /* ChessMatches = tournament.Rounds*/

            };

            return View(viewModel);
        }

        private async Task PopulateTournamentViewDataAsync(Tournament tournament)
        {
            if (User.IsInRole("admin"))
            {
                ViewData["OrganizerId"] = new SelectList(
                    _context.Organizers.Select(o => new
                    {
                        o.Id,
                        DisplayText = o.FirstName + " " + o.LastName
                    }), "Id", "DisplayText", tournament.OrganizerId);
            }
            else if (User.IsInRole("organizer"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.UserId == userId);
                ViewData["OrganizerId"] = new SelectList(new[]
                {
            new { Id = organizer.Id, DisplayText = organizer.FirstName + " " + organizer.LastName }
        }, "Id", "DisplayText", tournament.OrganizerId);
            }

            ViewData["TimeControlId"] = new SelectList(
                _context.TimeControls.Select(tc => new
                {
                    tc.Id,
                    DisplayText = $"{tc.BaseMinutes} | {tc.IncSeconds}"
                }), "Id", "DisplayText", tournament.TimeControlId);

            ViewData["TournamentTypeId"] = new SelectList(
                _context.TournamentTypes, "Id", "Name", tournament.TournamentTypeId);

            ViewData["VenueId"] = new SelectList(
                _context.Venues, "Id", "Name", tournament.VenueId);
        }


        // GET: Tournaments/Create
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Create()
        {
            await PopulateTournamentViewDataAsync(new Tournament());

            return View();
        }


        // POST: Tournaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Create(Tournament tournament)
        {
            if (User.IsInRole("organizer"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.UserId == userId);

                if (organizer == null)
                {
                    return Unauthorized();
                }

                ModelState.Remove("OrganizerId");
                tournament.OrganizerId = organizer.Id;
            }

            ModelState.Remove("IsOpen");
            tournament.IsOpen = true;

            if (ModelState.IsValid)
            {
                var existingTournament = await _context.Tournaments.FirstOrDefaultAsync(p => p.Name == tournament.Name);
                if (existingTournament != null)
                {
                    ModelState.AddModelError("", "Івент із такою назвою вже існує.");
                    return View(tournament);
                }

                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await PopulateTournamentViewDataAsync(tournament);

            return View(tournament);
        }



        // GET: Tournaments/Edit/5
        [Authorize(Roles = "admin, organizer")]
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!User.IsInRole("admin"))
            {
                var organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.UserId == userId);
                if (organizer == null || tournament.OrganizerId != organizer.Id)
                {
                    return Forbid();
                }
            }
            await PopulateTournamentViewDataAsync(tournament);

            return View(tournament);
        }


        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Edit(int id, [Bind("Name,TournamentTypeId,StartTime,EndTime,IsOnline,IsOpen,PlayerLimit,RoundCount,Link,VenueId,TimeControlId,OrganizerId,Description,Id")] Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return NotFound();
            }
            if (User.IsInRole("organizer"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.UserId == userId);

                if (organizer == null)
                {
                    return Unauthorized();
                }

                ModelState.Remove("OrganizerId");
                tournament.OrganizerId = organizer.Id;
            }


            ModelState.Remove("IsOpen");
            tournament.IsOpen = true;

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

            await PopulateTournamentViewDataAsync(tournament);
            return View(tournament);
        }

        // GET: Tournaments/Delete/5
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.Organizer)
                .Include(t => t.TimeControl)
                .Include(t => t.TournamentType)
                .Include(t => t.Venue)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!User.IsInRole("admin"))
            {
                var organizer = await _context.Organizers.FirstOrDefaultAsync(o => o.UserId == userId);
                if (organizer == null || tournament.OrganizerId != organizer.Id)
                {
                    return Forbid();
                }
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, organizer")]
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
