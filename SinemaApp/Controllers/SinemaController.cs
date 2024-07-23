using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;
namespace SinemaApp.Controllers
{
    public class SinemaController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        public IActionResult Index()
        {
            List<Sinema> sinemalar = db.Sinemas.ToList();
            return View(sinemalar);
        }

        [HttpGet]
        public IActionResult Detay(int id)
        {
            Film film = db.Films.FirstOrDefault(x => x.Id == id);
            List<Gosterim> gosterimler = db.Gosterims.Where(x => x.FilmId == id).ToList();
            ViewBag.film = film;
            ViewBag.gosterimler = gosterimler;
            return View();
        }

       
    }
}
