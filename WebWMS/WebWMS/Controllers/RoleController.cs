using AutoMapper;
using CommonLibraries.Excel;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebWMS.Common;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.CustomersService;
using WebWMS.Core.Services.RolesService;
using WebWMS.Models;

namespace WebWMS.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IOptionsSnapshot<ExceConfig> options;
        private readonly RedisClientHelper redisClient;
        private readonly IMapper mapper;

        public RoleController(IRoleService  roleService, IOptionsSnapshot<ExceConfig> options, RedisClientHelper redisClient, IMapper mapper)
        {
            this.roleService = roleService;
            this.options = options;
            this.redisClient = redisClient;
            this.mapper = mapper;
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

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Add(RoleViewModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var roeldto = mapper.Map<RoleDto>(model);
                roeldto.RoleName = model.RoleName;
                roeldto.RoleCode = model.RoleCode;
                roeldto.CreateTime = DateTime.Now.ToString();
                if (await roleService.GetRoleByNameAsync(roeldto.RoleCode, roeldto.RoleName))
                {
                    result.Message = "角色编号或名称已存在不能重复添加!";
                    result.Status = -1;
                }
                int num = await roleService.InsertRoleAsync(roeldto);
                if (num > 0)
                {
                    result.Message = "新增成功!";
                    result.Status = 200;
                }
                else
                {
                    result.Status = -1; result.Message = "新增失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1; result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Update(RoleViewModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var roledto = mapper.Map<RoleDto>(model);
                roledto.RoleName = model.RoleName;
                roledto.RoleCode = model.RoleCode;
                roledto.UpdateTime = DateTime.Now.ToString();
                var Customer = mapper.Map<RoleDto>(roledto);
                int num = await roleService.UpdateRoleAsync(Customer);
                if (num > 0)
                {
                    result.Message = "修改成功!";
                    result.Status = 200;
                    await redisClient.DeleteHashOfAsync("WMS_MenuList", "menuhash");
                }
                else
                {
                    result.Status = -1; result.Message = "修改失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1; result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> Delete(int id)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                int num = await roleService.DeleteRoleAsync(id);
                if (num > 0)
                {
                    result.Message = "删除成功!";
                    result.Status = 200;

                }
                else
                {
                    result.Status = -1; result.Message = "删除失败!";
                }
            }
            catch (Exception ex)
            {
                result.Status = -1; result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
