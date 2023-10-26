using Microsoft.AspNetCore.Mvc;

namespace WebWMS.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
