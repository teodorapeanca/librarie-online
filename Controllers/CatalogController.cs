using Microsoft.AspNetCore.Mvc;

namespace LibrarieOnline.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detalii()
        {
            return View();
        }
    }
}