using Microsoft.AspNetCore.Mvc;

namespace WebWMS.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
