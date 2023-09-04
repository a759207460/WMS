using Microsoft.AspNetCore.Mvc;

namespace WebWMS.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
