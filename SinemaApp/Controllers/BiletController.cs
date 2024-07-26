using Microsoft.AspNetCore.Http.HttpResults;
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

        public IActionResult BiletAl([FromBody] ReservationRequest requestData)
        {
            try
            {
                Kullanici kullanici = db.Kullanicis.FirstOrDefault(x => x.Isim == requestData.kullaniciAdi);
                Rezervasyon rezervasyon = new Rezervasyon();
                rezervasyon.KullaniciId = kullanici.Id;
                rezervasyon.GosterimId = requestData.gosterimId;

                db.Rezervasyons.Add(rezervasyon);
                db.SaveChanges();

                foreach (int koltukId in requestData.koltuklar)
                {

                    GösterimKoltuk koltuk = db.GösterimKoltuks.FirstOrDefault(x => x.Id == koltukId);
                    koltuk.RezervasyonId = rezervasyon.Id;
                    koltuk.Durum = true;
                    db.GösterimKoltuks.Update(koltuk);
                    db.SaveChanges();

                }

                return Ok(new {success = true , rezervasyonId = rezervasyon.Id});

            }
            catch(Exception e)
            {
                return BadRequest(new {success = false});
            }
           

        }

        public IActionResult Detay(int id)
        {
            Rezervasyon rezervasyon = db.Rezervasyons.FirstOrDefault(x =>x.Id == id);
            List<GösterimKoltuk> koltuks = db.GösterimKoltuks.Where(x=> x.RezervasyonId == id).ToList();
            Gosterim gosterim = db.Gosterims.FirstOrDefault(x => rezervasyon.GosterimId == x.Id);
            Salon salon = db.Salons.FirstOrDefault(x => x.Id == gosterim.SalonId);
            Film film = db.Films.FirstOrDefault(x => x.Id == gosterim.FilmId);
            Sinema sinema = db.Sinemas.FirstOrDefault(x => x.Id == salon.SinemaId);
            List<SinemaSalonuKoltuk> salonKoltuks = new List<SinemaSalonuKoltuk>();

            foreach(GösterimKoltuk koltuk in koltuks)
            {
                SinemaSalonuKoltuk salonKoltuk = db.SinemaSalonuKoltuks.FirstOrDefault(x => x.Id == koltuk.SinemaSalonuKoltukId);
                salonKoltuks.Add(salonKoltuk);
            }

            ViewBag.rezervasyon = rezervasyon; ViewBag.film = film;
            ViewBag.koltuklar = salonKoltuks;
            ViewBag.gosterim = gosterim;
            ViewBag.salon = salon;
            ViewBag.film = film;
            ViewBag.sinema = sinema;

            return View();
        }

    }

    public class ReservationRequest
    {
        public string kullaniciAdi { get; set; }
        public List<int> koltuklar { get; set; }
        public int gosterimId { get; set; }
    }
}
