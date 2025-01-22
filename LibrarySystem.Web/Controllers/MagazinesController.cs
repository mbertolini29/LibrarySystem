using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class MagazinesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
