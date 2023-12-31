﻿using AutoMapper;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Services.MenusService;
using WebWMS.Core.Services.RolesService;
using WebWMS.Core.Services.UserInfosService;
using WebWMS.Models;
using static CommonLibraries.Redis.RedisClientHelper;

namespace WebWMS.Common
{
    public class HelpGetMenuList
    {
        private readonly IMenuService menuService;
        private readonly IUserInfoService userInfoService;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;
        private readonly RedisClientHelper redisClient;
        private readonly ILogger<HelpGetMenuList> logger;

        public HelpGetMenuList(IMenuService menuService,IUserInfoService userInfoService,IRoleService roleService ,IMapper mapper, RedisClientHelper redisClient, ILogger<HelpGetMenuList> logger)
        {
            this.menuService = menuService;
            this.userInfoService = userInfoService;
            this.roleService = roleService;
            this.mapper = mapper;
            this.redisClient = redisClient;
            this.logger = logger;
        }

        //private static List<MenuModel> MenuList { get; set; } = new List<MenuModel>() {
        //         new MenuModel() {Title="首页",HasChildren=false, Url="/Home/Index?name=''",Tag="bi bi-house-door-fill",ParentName="Root",Name="" ,Style="nav nav-list collapse",HeadStyle="nav-header collapsed"},
        //         new MenuModel(){Title="基础信息",HasChildren=true, Url="#",Tag="bi bi-file-spreadsheet-fill",ParentName="Root",Name="user-menu",Style="nav nav-list collapse",HeadStyle="nav-header collapsed"},
        //         new MenuModel(){Title="设置信息",HasChildren=true,Url="#",Tag="bi bi-gear-fill",ParentName="Root",Name="setting-menu", Style = "nav nav-list collapse", HeadStyle = "nav-header collapsed"},
        //         new MenuModel(){Title="收货管理",HasChildren=false,Url="/Home/Index?name=''",Tag="bi bi-house-door-fill",ParentName="Root",Name="", Style = "nav nav-list collapse", HeadStyle = "nav-header collapsed"},
        //         new MenuModel(){Title="库存管理",HasChildren=false,Url="/Home/Index?name=''",Tag="bi bi-house-lock-fill" ,ParentName="Root",Name="", Style = "nav nav-list collapse", HeadStyle = "nav-header collapsed"},
        //         new MenuModel(){Title="仓内作业",HasChildren=true ,Url="#",Tag="bi bi-house-up-fill",ParentName="Root",Name="kurauchi-menu", Style = "nav nav-list collapse", HeadStyle = "nav-header collapsed"},
        //         new MenuModel(){Title="发货管理" ,HasChildren=false,Url="/Home/Index?name=''",Tag="bi bi-send-fill",ParentName="Root",Name="", Style = "nav nav-list collapse", HeadStyle = "nav-header collapsed"},

        //         new MenuModel(){Title="用户管理",ParentName="user-menu",NavigateController="User",Tag="bi bi-person-circle",Url="/User/Index?name='user-menu'"},
        //         new MenuModel(){Title="客户信息",ParentName="user-menu",NavigateController="User",Tag="bi bi-people",Url="/User/Index?name='user-menu'"},
        //         new MenuModel(){Title="货主信息",ParentName="user-menu",NavigateController="User",Tag="bi bi-person-square",Url="/User/Index?name='user-menu'"},
        //         new MenuModel(){Title="商品信息",ParentName="user-menu",NavigateController="User",Tag="bi bi-box-seam-fill",Url="/User/Index?name='user-menu'"},
        //         new MenuModel(){Title="公司信息",ParentName="user-menu",NavigateController="User",Tag="bi bi-buildings-fill", Url = "/User/Index?name='user-menu'"},
        //         new MenuModel(){Title="供应商信息",ParentName="user-menu",NavigateController="User",Tag="bi bi-person-square",Url="/User/Index?name='user-menu'"},
        //         //设置信息
        //         new MenuModel(){Title="角色设置",ParentName="setting-menu",NavigateController="User",Tag="bi bi-person-gear" , Url = "/User/Index?name='setting-menu'"},
        //         new MenuModel(){Title="菜单设置",ParentName="setting-menu",NavigateController="Menu",Tag="bi bi-menu-up" , Url = "/Menu/Index"},
        //         new MenuModel(){Title="仓库设置",ParentName="setting-menu",NavigateController="User",Tag="bi bi-house-gear-fill", Url = "/User/Index?name='setting-menu'"},
        //         new MenuModel(){Title="运费设置",ParentName="setting-menu",NavigateController="User",Tag="bi bi-cash", Url = "/User/Index?name='setting-menu'"},
        //         new MenuModel(){Title="商品列表设置",ParentName="setting-menu",NavigateController="User",Tag="bi bi-list-ul", Url = "/User/Index?name='setting-menu'"},
        //         //仓内作业
        //         new MenuModel(){Title="仓内加工",ParentName="kurauchi-menu",NavigateController="User",Tag="bi bi-house-up", Url = "/User/Index?name='kurauchi-menu'"},
        //         new MenuModel(){Title="库存移动",ParentName="kurauchi-menu",NavigateController="User",Tag="bi bi-houses", Url = "/User/Index?name='kurauchi-menu'"},
        //         new MenuModel(){Title="库存冻结",ParentName="kurauchi-menu",NavigateController="User",Tag="bi bi-house-lock-fill", Url = "/User/Index?name='kurauchi-menu'"},
        //         new MenuModel(){Title="库存调整",ParentName="kurauchi-menu",NavigateController="User",Tag="bi bi-house-exclamation-fill", Url = "/User/Index?name='kurauchi-menu'"},
        //         new MenuModel(){Title="库存盘点",ParentName="kurauchi-menu",NavigateController="User",Tag="bi bi-house-x-fill", Url = "/User/Index?name='kurauchi-menu'"},
        //};


        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetMenuList(string trigerName,int id)
        {
            try
            {
                var user =await userInfoService.GetCustomerByIdAsync(id);
                StringBuilder stringBuilder = new StringBuilder("<ul>");
                List<int> ids = user.Roles.Select(r => r.RoleId).ToList();
                List<MenuDto> list = null;
                if (redisClient.ExistsHash("WMS_MenuList_"+ user.Account, "menuhash"))
                {
                    list = redisClient.GetObjHash<List<MenuDto>>("WMS_MenuList_" + user.Account, "menuhash");
                }
                else
                {
                    list = await menuService.GetMenusByRoleIdsAsync(ids);
                    if(list != null&& list.Count()>0)
                    {
                        redisClient.SetObjHash<List<MenuDto>>("WMS_MenuList_" + user.Account, "menuhash", list, SerializeType.Json);
                        TimeSpan timeSpan = TimeSpan.FromHours(24);
                        redisClient.SetKeyExpire("WMS_MenuList_" + user.Account, timeSpan);
                    }
                }
                if (list != null && list.Count() > 0)
                {
                    var MenuList = mapper.Map<List<MenuModel>>(list);
                    List<MenuModel> FirstMenu = MenuList.Where(m => m.ParentName == "Root").ToList();
                    List<MenuModel> ChlidrenMenu = MenuList.Where(m => m.ParentName != "Root").ToList();
                    if (MenuList.Count() > 0)
                    {
                        foreach (var first in FirstMenu)
                        {
                            if (first.HasChildren)
                            {
                                if (!string.IsNullOrWhiteSpace(trigerName) && trigerName.Contains(first.Name))
                                {
                                    first.Style = "nav nav-list collapse in";
                                    first.HeadStyle = "nav-header";
                                }
                                else
                                {
                                    first.Style = "nav nav-list collapse";
                                    first.HeadStyle = "nav-header collapsed";
                                }
                                stringBuilder.Append($"<li><a href=\"{first.Url}\"  style=\"color:#444;\" data-target=\".{first.Name}\"  class=\"{first.HeadStyle}\" data-toggle=\"collapse\">{first.Tag} {first.Title}<i class=\"fa fa-collapse\"></i></a></li>\r\n<li>\r\n<ul class=\"{first.Name} {first.Style}\">");
                                foreach (var child in ChlidrenMenu)
                                {
                                    if (child.ParentName == first.Name)
                                    {
                                        stringBuilder.Append($"<li><a href=\"{child.Url}\" style=\"color:#444;\"><span class=\"fa fa-caret-right\"></span>{child.Tag} {child.Title}</a></li>");
                                    }
                                }
                                stringBuilder.Append("</ul>");
                            }
                            else
                            {
                                stringBuilder.Append($" <li><a href=\"{first.Url}\"  style=\"color:#444;\" class=\"{first.HeadStyle}\" >{first.Tag} {first.Title}</a></li>");
                            }
                        }
                    }
                }
                stringBuilder.Append("</ul>");
                return stringBuilder.ToString();

            }
            catch (Exception ex)
            {
                logger.LogError("菜单列表获取错误:", ex.Message);
                return "";
            }

        }
    }
}
