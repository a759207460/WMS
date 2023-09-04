using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebWMS.Common;
using WebWMS.Models;

namespace WebWMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetMenuList(string name)
        {
            string menuList = HelpGetMenuList.GetMenuList(name);
            return menuList;
        }


    }
}