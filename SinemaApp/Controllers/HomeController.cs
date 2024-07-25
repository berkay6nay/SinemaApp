using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinemaApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SinemaApp.Controllers
{
    public class HomeController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Giris(Kullanici k)
        {
            Kullanici u = db.Kullanicis.FirstOrDefault(x => x.Isim == k.Isim && x.Sifre == k.Sifre);
            if(u != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,u.Isim),
                new Claim(ClaimTypes.Role, u.Rol ?? "K")
            };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();

                return RedirectToAction("Index"); // Or wherever you want to redirect after login
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Cikis()
        {
            // Sign out the user
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            // Redirect to the home page or login page after logout
            return RedirectToAction("Index", "Home"); // Change this to the page you want to redirect to after logout
        }


        public IActionResult Index()
        {
            var filmler = db.Films
            .Include(film => film.Gosterims)
            .Where(film => film.Gosterims.Any())
            .ToList();
            return View(filmler);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
