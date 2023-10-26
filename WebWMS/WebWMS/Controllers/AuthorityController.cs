using AutoMapper;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;
using WebWMS.Common;
using WebWMS.Core.Domain.Menus;
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
        public async Task<string> GetMenusList(int rid)
        {
            ResultMessage<string> result = new ResultMessage<string>();
            StringBuilder builder = new StringBuilder();
            List<MenuDto> list = null;
            try
            {
                var role = await roleService.GetRoleByIdAsync(rid);
                if (redisClient.ExistsHash("WMS_MenuList_All", "menuhash"))
                {
                    list = redisClient.GetObjHash<List<MenuDto>>("WMS_MenuList_All", "menuhash");
                }
                else
                {
                    list = await menuService.GetAllAsync();
                    redisClient.SetObjHash<List<MenuDto>>("WMS_MenuList_All", "menuhash", list, SerializeType.Json);
                    TimeSpan timeSpan = TimeSpan.FromHours(24);
                    redisClient.SetKeyExpire("WMS_MenuList_All", timeSpan);
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
                                        if (role.Menus.Where(m => m.MenuId == child.Id).Count() > 0)
                                        {
                                            builder.Append($"<input type=\"checkbox\" checked=\"checked\" id=\"check_{child.Id}\"  value=\"{child.Id}\" style=\" margin-left:5px;\"/><span>{child.Title}</span>");
                                        }else
                                        {
                                            builder.Append($"<input type=\"checkbox\"  id=\"check_{child.Id}\"  value=\"{child.Id}\" style=\" margin-left:5px;\"/><span>{child.Title}</span>");
                                        }

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
                                    if (role.Menus.Where(m => m.MenuId == first.Id).Count() > 0)
                                    {
                                        builder.Append($"<input type=\"checkbox\" checked=\"checked\" id=\"check_{first.Id}\"  value=\"{first.Id}\" style=\" margin-left:5px;\"/><span>{first.Title}</span>");
                                    }
                                    else
                                    {
                                        builder.Append($"<input type=\"checkbox\"  id=\"check_{first.Id}\"  value=\"{first.Id}\" style=\" margin-left:5px;\"/><span>{first.Title}</span>");
                                    }
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
                                if (role.Menus.Where(m => m.MenuId == first.Id).Count() > 0)
                                {
                                    builder.Append($"<input type=\"checkbox\" checked=\"checked\" id=\"check_{first.Id}\"  value=\"{first.Id}\" style=\" margin-left:5px;\"/><span>{first.Title}</span>");
                                }
                                else
                                {
                                    builder.Append($"<input type=\"checkbox\"  id=\"check_{first.Id}\"  value=\"{first.Id}\" style=\" margin-left:5px;\"/><span>{first.Title}</span>");
                                }
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

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<string> AddAuthority(List<int> list, int rid)
        {
            ResultMessage<string> result = new ResultMessage<string>();
            try
            {
                RoleDto role = await roleService.GetRoleByIdAsync(rid);
                List<MenuDto> menus = await menuService.GetMenusByIdsAsync(list);
                List<int> plist = menus.Where(m => m.ParentId>0).Select(m =>m.ParentId ).Distinct().ToList();
                if (menus == null || menus.Count == 0)
                {
                    result.Status = -1;
                    result.Message = "未找到相关菜单信息!";
                    return JsonConvert.SerializeObject(result);
                }
               
                if (role != null)
                {
                    role.Menus = menus.Select(r => new MenuRole { MenuId = r.Id, RoleId = rid }).ToList();
                    if (plist != null && plist.Count > 0)
                    {
                        for (int i = 0; i < plist.Count(); i++)
                        {
                            role.Menus.Add(new MenuRole { MenuId = plist[i], RoleId = rid });
                        }
                    }
                    int num = await roleService.UpdateRoleAsync(role);
                    if (num > 0)
                    {
                        result.Status = 200;
                        result.Message = "权限分配成功!";
                        redisClient.RemoveByPattern("WMS_MenuList");
                    }
                    else
                    {
                        result.Status = -1;
                        result.Message = "权限分配失败!";
                    }
                }
                else
                {
                    result.Status = -1;
                    result.Message = "未找到角色相关信息!";
                }
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
