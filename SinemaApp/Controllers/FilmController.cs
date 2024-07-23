using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;

namespace SinemaApp.Controllers
{
    public class FilmController : Controller
    {
        Sinema2Context db = new Sinema2Context();

        public IActionResult Index()
        {
            List<Film> filmler = db.Films.ToList();
            return View(filmler);
        }

        public string Guncelle(int id , String Isim , String Aciklama, int Sure , String Dil , String Tur , DateTime CikisTarihi) 
        {
            Film film = new Film();
            film.Id = id;
            film.Isim = Isim;
            film.Aciklama = Aciklama;
            film.Sure = Sure;
            film.Dil = Dil;
            film.Tur = Tur;
            film.CikisTarihi = CikisTarihi;
            try
            {
                db.Films.Update(film);
                db.SaveChanges();
                return "basarili";
            }
            catch
            {
                return "hata";
            }
            
        }

        [HttpPost]
        public string Ekle(String Isim, String Aciklama, int Sure, String Dil, String Tur, DateTime CikisTarihi)
        {
            Film film = new Film
            {
                Isim = Isim,
                Aciklama = Aciklama,
                Sure = Sure,
                Dil = Dil,
                Tur = Tur,
                CikisTarihi = CikisTarihi
            };

            try
            {
                db.Films.Add(film);
                db.SaveChanges();
                return "basarili";
            }
            catch(Exception e)
            {
                return "hata";
            }
            
        }
    }
}