using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChessinatorDomain.Model;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using ChessinatorInfrastructure.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Metadata.Profiles.Xmp;


namespace ChessinatorInfrastructure.Controllers
{
    public class OrganizersController : Controller
    {
        private readonly ChessdbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public OrganizersController(ChessdbContext context, SignInManager<User> signInManager, UserManager<User> userManager, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Organizers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizers.ToListAsync());
        }

        // GET: Organizers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var organizer = await _context.Organizers
     .Include(o => o.Tournaments)!
         .ThenInclude(t => t.TournamentType)
     .Include(o => o.Tournaments)!
         .ThenInclude(t => t.Venue)
     .Include(o => o.Tournaments)!
         .ThenInclude(t => t.TimeControl)
     .FirstOrDefaultAsync(o => o.Id == id);

            if (organizer == null)
            {
                return NotFound();
            }

            var viewModel = new OrgDetailsViewModel
            {
                Organizer = organizer,
                Tournaments = organizer.Tournaments
            };

            return View(viewModel);
        }

        // GET: Organizers/Edit/5
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Edit(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var myProfile = await _context.Organizers
                .FirstOrDefaultAsync(o => o.UserId == userId);

            if (myProfile == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && myProfile.Id != id)
            {
                return RedirectToAction("Edit", new { id = myProfile.Id });
            }

            var organizer = await _context.Organizers
                .FirstOrDefaultAsync(o => o.Id == id);

            if (organizer == null)
            {
                return NotFound();
            }

            var tournaments = await _context.Tournaments
                .Where(t => t.OrganizerId == organizer.Id)
                .ToListAsync();

            var model = new OrgDetailsViewModel
            {
                Organizer = organizer,
                Tournaments = tournaments
            };

            return View(model);
        }


        /*        // POST: Organizers/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                [Authorize(Roles = "admin, organizer")]
                public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,Email,Detais,Organization,UserId,Id")] Organizer organizer)
                {
                    if (id != organizer.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(organizer);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!OrganizerExists(organizer.Id))
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
                    return View(organizer);
                }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Edit(int id, OrgDetailsViewModel model)
        {
            if (id != model.Organizer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (model.ProfilePicture != null)
                {
                    using (var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(model.ProfilePicture.OpenReadStream()))
                    {
                        if (image.Width > 150 || image.Height > 150)
                        {
                            ModelState.AddModelError("ProfilePicture", "The image must be smaller than 150x150 pixels.");
                            return View(model);
                        }
                    }

                    var fileName = Path.GetFileName(model.ProfilePicture.FileName);
                    var relativePath = Path.Combine("images", "profileimages", fileName);
                    var fullPath = Path.Combine(_hostingEnvironment.WebRootPath, relativePath);
                    using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        await model.ProfilePicture.CopyToAsync(stream);
                    }


                    var orgfilename = "/" + Path.GetFileName(model.ProfilePicture.FileName);

                    if (!string.IsNullOrEmpty(model.Organizer.ProfilePicturePath))
                    {
                        var oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, model.Organizer.ProfilePicturePath.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    model.Organizer.ProfilePicturePath = "/" + relativePath.Replace("\\", "/");
                }

                try
                {
                    _context.Update(model.Organizer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizerExists(model.Organizer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = model.Organizer.Id });
            }
            return View(model);
        }

        // GET: Organizers/Delete/5
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> Delete(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var myProfile = await _context.Organizers
                .FirstOrDefaultAsync(o => o.UserId == userId);

            if (myProfile == null)
            {
                return NotFound();
            }

            if (!User.IsInRole("Admin") && myProfile.Id != id)
            {
                return RedirectToAction("Delete", new { id = myProfile.Id });
            }
            var organizer = await _context.Organizers
                .FirstOrDefaultAsync(o => o.Id == id);

            if (organizer == null)
            {
                return NotFound();
            }
            return View(organizer);
        }

        // POST: Organizers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin, organizer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizer = await _context.Organizers.FindAsync(id);

            if (organizer != null)
            {
                _context.Organizers.Remove(organizer);
                await _context.SaveChangesAsync();

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (organizer.UserId == userId && User.IsInRole("organizer"))
                {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account"); 
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrganizerExists(int id)
        {
            return _context.Organizers.Any(e => e.Id == id);
        }
    }
}
