using Microsoft.AspNetCore.Mvc;

namespace LibrarieOnline.Controllers
{
    public class CosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}