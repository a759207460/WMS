using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebWMS.Common;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.RolesService;

namespace WebWMS.Controllers
{
    [Authorize]
    public class AuthorityController : Controller
    {
        private readonly IRoleService roleService;

        public AuthorityController(IRoleService roleService)
        {
            this.roleService = roleService;
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
        public async Task<string> GetRoleList(RequestModel model)
        {
            ResultMessage<IPagedList<RoleDto>> result = new ResultMessage<IPagedList<RoleDto>>();
            try
            {
                result.Status = 200;
                result.Source = await roleService.GetAllPagedListAsync(model.pageIndex - 1, model.pageSize, model.Where);
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
