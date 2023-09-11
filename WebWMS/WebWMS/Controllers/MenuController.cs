using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebWMS.Common;
using WebWMS.Core.DTO.Customers;
using WebWMS.Core.DTO.Rolemenus;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.CustomersService;
using WebWMS.Core.Services.RolemenusService;

namespace WebWMS.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> GetMenuList(RequestModel model)
        {
            ResultMessage<IPagedList<MenuDto>> result = new ResultMessage<IPagedList<MenuDto>>();
            try
            {
                result.Status = 200;
                result.Source = await menuService.GetAllAsync(model.pageIndex - 1, model.pageSize, model.Where);
            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }
            string js = JsonConvert.SerializeObject(result);
            return js;
        }
    }
}
