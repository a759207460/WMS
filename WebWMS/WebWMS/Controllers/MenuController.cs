﻿using AutoMapper;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using WebWMS.Common;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.MenusService;
using WebWMS.Models;

namespace WebWMS.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService menuService;
        private readonly RedisClientHelper redisClient;
        private readonly IMapper mapper;

        public MenuController(IMenuService menuService, RedisClientHelper redisClient, IMapper mapper)
        {
            this.menuService = menuService;
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
        public async Task<string> GetMenuList(RequestModel model)
        {
            ResultMessage<IPagedList<MenuDto>> result = new ResultMessage<IPagedList<MenuDto>>();
            try
            {
                result.Status = 200;
                result.Source = await menuService.GetAllPagedListAsync(model.pageIndex - 1, model.pageSize, model.Where);
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
        /// 获取父菜单
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMenuListByParentId()
        {
            ResultMessage<List<SelectMenuDto>> result = new ResultMessage<List<SelectMenuDto>>();
            try
            {
                var list = await menuService.GetMenusByParentIdAsync(0);
                var mlist = list?.Select(m => new SelectMenuDto { Id = m.Id, Name = m.Title }).ToList();
                result.Status = 200;
                result.Source = mlist;
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
        public async Task<string> Add(MenuModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var plist = await menuService.GetMenusByParentIdAsync(0);
                var pname = plist.Where(m => m.Title == model.ParentName).FirstOrDefault();
                var menuto = mapper.Map<MenuDto>(model);
                string? user=User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                menuto.Name = model.Name;
                menuto.Title = model.Title;
                menuto.NavigateController = model.NavigateController;
                menuto.NavigateActioin = model.NavigateActioin;
                menuto.Tag = model.Tag;
                menuto.ParentName = pname?.Name;
                menuto.HasChildren = model.HasChildren;
                menuto.HeadStyle = model.HeadStyle;
                menuto.Style = model.Style;
                menuto.Sort=model.Sort;
                menuto.Creator = user;
                menuto.CreateTime = DateTime.Now.ToString();
                if (await menuService.GetMenuByNameAsync(menuto.Name, model.Title))
                {
                    result.Message = "菜单名称或标题已存在不能重复添加!";
                    result.Status = -1;
                }
                int num = await menuService.InsertMenuAsync(menuto);
                if (num > 0)
                {
                    result.Message = "新增成功!";
                    result.Status = 200;
                    redisClient.RemoveByPattern("WMS_MenuList");
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
        public async Task<string> Update(MenuModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var plist = await menuService.GetMenusByParentIdAsync(0);
                var menuto = await menuService.GetMenuByIdAsync(model.Id);
                var pname = plist.Where(m => m.Title == model.ParentName).FirstOrDefault();
                menuto.Name = model.Name;
                menuto.Title = model.Title;
                menuto.NavigateController = model.NavigateController;
                menuto.NavigateActioin = model.NavigateActioin;
                menuto.Tag = model.Tag;
                menuto.ParentName = pname?.Name;
                menuto.HasChildren = model.HasChildren;
                menuto.HeadStyle = model.HeadStyle;
                menuto.Style = model.Style;
                menuto.Url = model.Url;
                menuto.ParentId = model.ParentId;
                menuto.Sort=model.Sort;
                menuto.UpdateTime = DateTime.Now.ToString();
                var menu = mapper.Map<MenuDto>(menuto);
                int num = await menuService.UpdateMenuAsync(menu);
                if (num > 0)
                {
                    result.Message = "修改成功!";
                    result.Status = 200;
                    //List<string> list = redisClient.Getkeys("WMS_MenuList");
                    redisClient.RemoveByPattern("WMS_MenuList");
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
                int num = await menuService.DeleteMenuAsync(id);
                if (num > 0)
                {
                    result.Message = "删除成功!";
                    result.Status = 200;
                    redisClient.RemoveByPattern("WMS_MenuList");
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
