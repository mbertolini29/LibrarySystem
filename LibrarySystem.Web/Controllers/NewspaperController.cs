using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class NewspaperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
