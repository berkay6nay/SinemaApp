using Microsoft.AspNetCore.Mvc;
using SinemaApp.Models;
using System.Diagnostics;

namespace SinemaApp.Controllers
{
    public class HomeController : Controller
    {
        Sinema2Context db = new Sinema2Context();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Film> filmler = db.Films.ToList();
            return View(filmler);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
