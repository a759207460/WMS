using Microsoft.AspNetCore.Mvc;
using WebWMS.Common;

namespace WebWMS.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            return View("Index");
        }
    }
}
