using ChessinatorDomain.Model;
using ChessinatorInfrastructure.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChessinatorInfrastructure.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ChessdbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ChessdbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpGet]
        public IActionResult RegisterPlayer()
        {
            return View();
        }


        [HttpGet]
        public IActionResult RegisterOrganizer()
        {
            return View();
        }

        // пост-метод для реєстрації гравця
        [HttpPost]
        public async Task<IActionResult> RegisterPlayer(RegisterPlayerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "player");

                    var untitled = await _context.Titles.FirstOrDefaultAsync(t => t.ShortName == "Untitled");
                    var existingPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Username == model.Username);
                    if (existingPlayer != null)
                    {
                        ModelState.AddModelError("", "This username is already taken.");
                        return View(model);
                    }


                    // створення відповідного гравця
                    var player = new Player
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Username = model.Username,
                        Birthday = model.Birthday,
                        Details = model.Details,
                        TitleId = untitled.Id,
                        CurrentElo = 400,
                        PeakElo = 400,
                        TotalGamesCount = 0,
                        UserId = user.Id
                    };

                    _context.Players.Add(player);
                    await _context.SaveChangesAsync();

                    user.PlayerId = player.Id;
                    await _userManager.UpdateAsync(user);

                    TempData["RegistrationSuccess"] = "Ви успішно зареєструвались!";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        // пост-метод для реєстрації організатора
        [HttpPost]
        public async Task<IActionResult> RegisterOrganizer(RegisterOrganizerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "organizer");

                    // створення відповідного організатора
                    var organizer = new Organizer
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Organization = model.Organization,
                        Email = model.Email,
                        Detais = model.Details,
                        UserId = user.Id
                    };

                    _context.Organizers.Add(organizer);
                    await _context.SaveChangesAsync();

                    user.OrganizerId = organizer.Id;
                    await _userManager.UpdateAsync(user);

                    TempData["RegistrationSuccess"] = "Ви успішно зареєструвались!";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null && user.OrganizerId.HasValue)
                    {
                        var claims = await _userManager.GetClaimsAsync(user);
                        if (!claims.Any(c => c.Type == "OrganizerId"))
                        {
                            await _userManager.AddClaimAsync(user, new Claim("OrganizerId", user.OrganizerId.Value.ToString()));

                            await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        }
                    }
                    else if (user != null && user.PlayerId.HasValue)
                    {
                        var claims = await _userManager.GetClaimsAsync(user);
                        if (!claims.Any(c => c.Type == "PlayerId"))
                        {
                            await _userManager.AddClaimAsync(user, new Claim("PlayerId", user.PlayerId.Value.ToString()));

                            await _signInManager.SignInAsync(user, isPersistent: model.RememberMe);
                        }
                    }
                        // перевіряємо, чи належить URL додатку
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Tournaments");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильний логін чи (та) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // видаляємо автентифікаційні куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
