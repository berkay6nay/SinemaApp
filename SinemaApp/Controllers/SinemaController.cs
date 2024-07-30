using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinemaApp.Models;
namespace SinemaApp.Controllers
{

    [Authorize(Roles = "A")]
    public class SinemaController : Controller
    {
        
        Sinema2Context db = new Sinema2Context();
        
        
        public IActionResult Index()
        {
            List<Sinema> sinemalar = db.Sinemas.ToList();
            return View(sinemalar);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Detay(int id) //kullanıcılar için gösterim listeleme sayfası
        {
            Film film = db.Films.FirstOrDefault(x => x.Id == id);

            // Retrieve the showings with related film, salon, and sinema data
            List<Gosterim> gosterimler = db.Gosterims
                .Include(g => g.Film)      // Include related Film data
                .Include(g => g.Salon)     // Include related Salon data
                    .ThenInclude(salon => salon.Sinema) // Include related Sinema data through Salon
                .Where(x => x.FilmId == id)
                .ToList();

            // Pass data to the view
            ViewBag.film = film;
            ViewBag.gosterimler = gosterimler;
            return View();
        }
        [HttpPost]
        public string Guncelle(int Id , string Isim , string Adres)
        {
            Sinema sinema = new Sinema
            {
                Id = Id,
                Isim = Isim,
                Adres = Adres
            };
            try
            {
                db.Sinemas.Update(sinema);
                db.SaveChanges();
                return "basarili";
            }
            catch
            {
                return "hata";
            }
        }

        [HttpPost]
        public string Ekle(string Isim , string Adres)
        {
            Sinema sinema = new Sinema();
            sinema.Isim = Isim;
            sinema.Adres = Adres;
            try
            {
                db.Sinemas.Add(sinema);
                db.SaveChanges();
                return "basarili";
            }
            catch
            {
                return "hata";
            }
        }
       
    }
}
