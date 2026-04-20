using Microsoft.AspNetCore.Mvc;

namespace LibrarieOnline.Controllers
{
    public class CategoriiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}