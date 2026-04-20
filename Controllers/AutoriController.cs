using Microsoft.AspNetCore.Mvc;

namespace LibrarieOnline.Controllers
{
    public class AutoriController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}