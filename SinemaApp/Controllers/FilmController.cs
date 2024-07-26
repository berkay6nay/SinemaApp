using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;

namespace SinemaApp.Controllers
{
    
    public class FilmController : Controller
    {
        Sinema2Context db = new Sinema2Context();

        [Authorize(Roles = "A" )]
        public IActionResult Index()
        {
            List<Film> filmler = db.Films.ToList();
            return View(filmler);
        }

        [Authorize(Roles = "A")]
        public string Guncelle(int Id , String Isim , String Aciklama, int Sure , String Dil , String Tur , DateTime CikisTarihi , String FotoUrl) 
        {
            Film film = new Film();
            film.Id = Id;
            film.Isim = Isim;
            film.Aciklama = Aciklama;
            film.Sure = Sure;
            film.Dil = Dil;
            film.Tur = Tur;
            film.CikisTarihi = CikisTarihi;
            film.FotoUrl = FotoUrl;
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

        [Authorize(Roles = "A")]
        [HttpPost]
        public string Ekle(String Isim, String Aciklama, int Sure, String Dil, String Tur, DateTime CikisTarihi ,IFormFile photo)
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
                if (photo != null && photo.Length > 0)
                {
                    var fileName = Path.GetFileName(photo.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                  
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }

                   
                    film.FotoUrl = $"/images/{fileName}";
                }

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