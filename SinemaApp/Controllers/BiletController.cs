using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;

namespace SinemaApp.Controllers
{
    public class BiletController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BiletAlmaEkrani(int id)
        {

            Gosterim gosterim = db.Gosterims.FirstOrDefault(x => x.Id == id);
            List<GösterimKoltuk> koltuklar = db.GösterimKoltuks.Where(x=> x.GosterimId == id).ToList();
            Film film = db.Films.FirstOrDefault(x => gosterim.FilmId == x.Id);
            Salon salon = db.Salons.FirstOrDefault(x => gosterim.SalonId == x.Id);
            Sinema sinema = db.Sinemas.FirstOrDefault(x => salon.SinemaId == x.Id);
            List<SinemaSalonuKoltuk> salonKoltuklar = new List<SinemaSalonuKoltuk>();
            foreach(GösterimKoltuk gösterimKoltuk in koltuklar)
            {
                SinemaSalonuKoltuk salonKoltuk = db.SinemaSalonuKoltuks.FirstOrDefault(x => x.Id == gösterimKoltuk.SinemaSalonuKoltukId);
                salonKoltuklar.Add(salonKoltuk);
            }

            ViewBag.gosterim = gosterim;
            ViewBag.koltuklar = koltuklar;
            ViewBag.film = film;
            ViewBag.salon = salon;
            ViewBag.sinema = sinema;
            ViewBag.salonKoltuklar = salonKoltuklar;

            return View();
        }

        public void BiletAl()
        {

        }

    }
}
