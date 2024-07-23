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

    }
}
