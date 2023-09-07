using Microsoft.AspNetCore.Mvc;

namespace WebWMS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
