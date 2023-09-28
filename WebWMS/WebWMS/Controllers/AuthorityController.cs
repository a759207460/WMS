using AutoMapper;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebWMS.Common;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.MenusService;
using WebWMS.Core.Services.RolesService;
using WebWMS.Models;
using static CommonLibraries.Redis.RedisClientHelper;

namespace WebWMS.Controllers
{
    [Authorize]
    public class AuthorityController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IMenuService menuService;
        private readonly RedisClientHelper redisClient;
        private readonly IMapper mapper;

        public AuthorityController(IRoleService roleService, IMenuService menuService, RedisClientHelper redisClient, IMapper mapper)
        {
            this.roleService = roleService;
            this.menuService = menuService;
            this.redisClient = redisClient;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取角色列表
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
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMenusList()
        {
            ResultMessage<string> result = new ResultMessage<string>();
            StringBuilder builder = new StringBuilder();
            List<MenuDto> list = null;
            try
            {
                if (redisClient.ExistsHash("WMS_MenuList", "menuhash"))
                {
                    list = redisClient.GetObjHash<List<MenuDto>>("WMS_MenuList", "menuhash");
                }
                else
                {
                    list = await menuService.GetAllAsync();
                    redisClient.SetObjHash<List<MenuDto>>("WMS_MenuList", "menuhash", list, SerializeType.Json);
                    TimeSpan timeSpan = TimeSpan.FromHours(24);
                    redisClient.SetKeyExpire("WMS_MenuList", timeSpan);
                }
                if (list != null && list.Count() > 0)
                {
                    var MenuList = mapper.Map<List<MenuModel>>(list);
                    List<MenuModel> FirstMenu = MenuList.Where(m => m.ParentName == "Root").ToList();
                    List<MenuModel> childrenList = null;
                    if (MenuList.Count() > 0)
                    {
                        foreach (var first in FirstMenu)
                        {
                            if (first.HasChildren)
                            {
                                builder.Append($"<p style=\"color:#4d5b76;font-size:1em;font-family:Cambria;font-weight:450;\">{first.Title}</p>");
                                childrenList = MenuList.Where(m => m.ParentName == first.Name).ToList();
                                if (childrenList != null && childrenList.Count() > 0)
                                {
                                    builder.Append("<div class=\"modal-body\" style=\"padding:5px;\">");
                                    builder.Append(" <div class=\"panel panel-default\"  >");
                                    builder.Append("<div class=\"container\" style=\"display:flex;flex-wrap:wrap;width:550px;\">");
                                    foreach (var child in childrenList)
                                    {
                                        builder.Append($"<input type=\"checkbox\" onchange=\"CheckBoxClick()\" value=\"{child.Id}\" style=\" margin-left:5px;\"/><span>{child.Title}</span>");
                                    }
                                    builder.Append("</div>");
                                    builder.Append("</div>");
                                    builder.Append("</div>");
                                }
                                else
                                {
                                    builder.Append("<div class=\"modal-body\" style=\"padding:5px;\">");
                                    builder.Append(" <div class=\"panel panel-default\"  >");
                                    builder.Append("<div class=\"container\" style=\"display:flex;flex-wrap:wrap;width:550px;\">");
                                    builder.Append($"<input type=\"checkbox\" onchange=\"CheckBoxClick()\" value=\"{first.Id}\" style=\" margin-left:5px;\"/><span>{first.Title}</span>");
                                    builder.Append("</div>");
                                    builder.Append("</div>");
                                    builder.Append("</div>");
                                }
                            }
                            else
                            {
                                builder.Append($"<p style=\"color:#4d5b76;font-size:1em;font-family:Cambria;font-weight:500;\">{first.Title}</p>");
                                builder.Append("<div class=\"modal-body\" style=\"padding:5px;\">");
                                builder.Append(" <div class=\"panel panel-default\"  >");
                                builder.Append("<div class=\"container\" style=\"display:flex;flex-wrap:wrap;width:550px;\">");
                                builder.Append($"<input type=\"checkbox\" onchange=\"CheckBoxClick()\" value=\"{first.Id}\" style=\" margin-left:5px;\"/><span>{first.Title}</span>");
                                builder.Append("</div>");
                                builder.Append("</div>");
                                builder.Append("</div>");
                            }
                        }
                    }
                }
                result.Status = 200;
                result.Source = builder.ToString();
            }
            catch (Exception ex)
            {
                result.Status = -1;
                result.Message = ex.Message;
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
