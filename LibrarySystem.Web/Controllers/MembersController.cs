using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Web.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
