using Microsoft.AspNetCore.Mvc;

namespace WebWMS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
