using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebWMS.Common;
using WebWMS.Models;

namespace WebWMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HelpGetMenuList helpGetMenuList;

        public HomeController(ILogger<HomeController> logger, HelpGetMenuList helpGetMenuList)
        {
            _logger = logger;
            this.helpGetMenuList = helpGetMenuList;
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
        public async Task<string> GetMenuList(string name)
        {
            string strId = User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            int id = string.IsNullOrWhiteSpace(strId)?0:Convert.ToInt32(strId);
            string menuList =await helpGetMenuList.GetMenuList(name, id);
            return menuList;
        }


    }
}