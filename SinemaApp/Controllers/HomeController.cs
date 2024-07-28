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
        public IActionResult Giris(string? hata)
        {
            if (!string.IsNullOrEmpty(hata) && hata == "yanlis")
            {
                ViewBag.HataMesaj = "Yanlýþ kullanýcý adý ya da þifre girdiniz.";
            }

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
                return RedirectToAction("Giris" , new { hata = "yanlis" });
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

        [HttpGet]
        public IActionResult KayitYap(string? hata)
        {
            if (!string.IsNullOrEmpty(hata) && hata == "kullaniciMevcut")
            {
                ViewBag.HataMesaj = "Bu isimde baþka bir kullanýcý mevcut. Lütfen baþka bir isim seçiniz";
            }
            return View();

        }
        [HttpPost]
        public IActionResult KayitYap(string Isim , string Sifre)
        {
            Kullanici kullanici = new Kullanici();
            kullanici.Isim = Isim;
            kullanici.Sifre = Sifre;
            kullanici.Rol = "K";
            try
            {
                db.Kullanicis.Add(kullanici);
                db.SaveChanges();
                return RedirectToAction("Giris" ,"Home");
            }
            catch
            {
                string hata = "kullaniciMevcut";
                return RedirectToAction("KayitYap", new { hata = "kullaniciMevcut" });
            }            
        }

        [HttpGet]
        public IActionResult FilmAra(string isim)
        {
            if (string.IsNullOrEmpty(isim))
            {
                return View(new List<Film>());
            }

            List<Film> films = db.Films
                      .Where(f => f.Isim.Contains(isim))
                      .ToList();
            return View(films);
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
