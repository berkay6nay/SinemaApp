using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinemaApp.Models;

namespace SinemaApp.Controllers
{
    [Authorize(Roles ="A")]
    public class GosterimController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        public IActionResult Index()
        {
            List<Film> filmler = db.Films.ToList();
            List<Sinema> sinemalar = db.Sinemas.ToList();            // Retrieve the showings with related film, salon, and sinema data
            List<Gosterim> gosterims = db.Gosterims
                            .Include(g => g.Salon)                  // Include the related Salon
                            .ThenInclude(s => s.Sinema)             // Include the related Sinema through Salon
                            .Include(g => g.Film)                   // Include the related Film
                            .ToList();

            ViewBag.Gosterims = gosterims;
            ViewBag.Sinemalar = sinemalar;
            ViewBag.Filmler = filmler;

            return View();
        }
        public string Ekle(Gosterim gosterim)
        {
            if (IsTimeSlotAvailable(gosterim))
            {
                db.Gosterims.Add(gosterim);
                db.SaveChanges();

                Salon salon = db.Salons.FirstOrDefault(x => x.Id == gosterim.SalonId);
                List<SinemaSalonuKoltuk> koltuklar = db.SinemaSalonuKoltuks.Where(x => x.SalonId == salon.Id).ToList();
                if (!koltuklar.Any())
                {
                    return "hata";
                }
                foreach(SinemaSalonuKoltuk koltuk in koltuklar)
                {
                    GösterimKoltuk gösterimKoltuk = new GösterimKoltuk();
                    gösterimKoltuk.Durum = false;
                    gösterimKoltuk.SinemaSalonuKoltukId = koltuk.Id;
                    gösterimKoltuk.Fiyat = 50;
                    gösterimKoltuk.GosterimId = gosterim.Id;
                    db.GösterimKoltuks.Add(gösterimKoltuk);
                    db.SaveChanges();
                }
                return "basarili"; 
            }
            else
            {
                return "Secilen zaman araligi icin salon dolu";
            }
        }
        public string Guncelle(Gosterim gosterim)
        {
            if (IsTimeSlotAvailable(gosterim))
            {
                try
                {
                    db.Gosterims.Update(gosterim);
                    db.SaveChanges();
                    return "basarili";
                }
                catch
                {
                    return "hata";
                }
            }
            else return "hata";
        }

        public IActionResult EklenenGosterimDetay(int id)
        {
            return View();
        }
        public string IptalEt(int id)
        {
            Gosterim gosterim = db.Gosterims.FirstOrDefault(g=>g.Id == id);
            List<GösterimKoltuk> koltuklar = db.GösterimKoltuks.Where(x => x.GosterimId == gosterim.Id).ToList();
            List<Rezervasyon> rezervasyonlar = db.Rezervasyons.Where(x => x.GosterimId == gosterim.Id).ToList();
            try
            {
                foreach(Rezervasyon rezervasyon in rezervasyonlar)
                {
                    db.Rezervasyons.Remove(rezervasyon);
                    db.SaveChanges();
                }
                foreach(GösterimKoltuk koltuk in koltuklar)
                {
                    db.GösterimKoltuks.Remove(koltuk);
                    db.SaveChanges();
                }

                db.Gosterims.Remove(gosterim);
                db.SaveChanges();
                return "basarili";
            }
            catch
            {
                return "hata";
            }
        }
        private bool IsTimeSlotAvailable(Gosterim newGosterim)
        {
            return !db.Gosterims.Any(g =>
                g.SalonId == newGosterim.SalonId &&
                (
                    (newGosterim.Baslangic >= g.Baslangic && newGosterim.Baslangic < g.Bitis) ||
                    (newGosterim.Bitis > g.Baslangic && newGosterim.Bitis <= g.Bitis) ||
                    (newGosterim.Baslangic <= g.Baslangic && newGosterim.Bitis >= g.Bitis)
                )
            );
        }

        

    }
}
