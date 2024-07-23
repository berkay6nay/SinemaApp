using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;

namespace SinemaApp.Controllers
{
    public class KoltukController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        public IActionResult Index()
        {
            return View();
        }
        public string Ekle(int Id)
        {
            String rows = "ABCDE";
            try
            {
                foreach (Char row in rows)
                {
                    for (int i = 1; i < 11; ++i)
                    {
                        SinemaSalonuKoltuk koltuk = new SinemaSalonuKoltuk();
                        koltuk.Sira = Convert.ToString(row);
                        koltuk.SiraNo = i;
                        koltuk.SalonId = Id;
                        db.SinemaSalonuKoltuks.Add(koltuk);
                        db.SaveChanges();
                    }
                }

                return "basarili";
            }
            catch (Exception ex)
            {
                return "hata";
            }
            
        }
    }
}
