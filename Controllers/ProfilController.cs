using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibrarieOnline.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}