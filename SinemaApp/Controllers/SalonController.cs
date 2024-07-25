using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;
namespace SinemaApp.Controllers
{
    public class SalonController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public string Ekle(int Id , String Isim)
        {
            Salon salon = new Salon();
            salon.SinemaId = Id;
            salon.Isim = Isim;
            try
            {
                db.Salons.Add(salon);
                db.SaveChanges();
                return "basarili";
            }
            catch (Exception ex) {
                return "hata";
            }
        }
        public IActionResult Listele(int id)
        {
     
            List<Salon> salons = db.Salons.Where(x=> x.SinemaId == id).ToList();
            
            return View(salons);
        }

        [HttpGet]
        public IActionResult Detay(int id) 
        {
            Salon salon = db.Salons.FirstOrDefault(x=>x.Id == id);
            List<SinemaSalonuKoltuk> koltuks = db.SinemaSalonuKoltuks.Where(x=> x.SalonId == id).ToList();
            List<Gosterim> gosterims = db.Gosterims.Where(x=> x.SalonId==id).ToList();
            Sinema sinema = db.Sinemas.FirstOrDefault(x => x.Id == salon.SinemaId);
                
            int koltuk_sayisi = koltuks.Count;

            ViewBag.Sinema = sinema;
            ViewBag.Salon = salon;
            ViewBag.koltukSayisi = koltuk_sayisi;
            ViewBag.gosterimler = gosterims;
            return View();
        }

        public IActionResult SalonGetir(int sinemaId)
        {

            var salons = db.Salons
        .Where(s => s.SinemaId == sinemaId)
        .Select(s => new { Id = s.Id, Isim = s.Isim })
        .ToList();

            foreach (var salon in salons)
            {
                Console.WriteLine($"Id: {salon.Id}, Isim: {salon.Isim}");
            }

            return Json(salons);
        }

    }
}
