using AutoMapper;
using CommonLibraries.Redis;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebWMS.Common;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.MenusService;
using WebWMS.Models;

namespace WebWMS.Controllers
{
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
        /// 新增用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> Add(MenuModel model)
        {
            ResultMessage result = new ResultMessage();
            try
            {
                var menuto = mapper.Map<MenuDto>(model);
                menuto.Name = model.Name;
                menuto.Title = model.Title;
                menuto.NavigateController = model.NavigateController;
                menuto.NavigateActioin=model.NavigateActioin;
                menuto.Tag= model.Tag;
                menuto.ParentName= model.ParentName;
                menuto.HasChildren = model.HasChildren;
                menuto.HeadStyle= model.HeadStyle;
                menuto.Style=model.Style;
                menuto.CreateTime = DateTime.Now.ToString();
                if (await menuService.GetMenuByNameAsync(menuto.Name,model.Title))
                {
                    result.Message = "菜单名称或标题已存在不能重复添加!";
                    result.Status = -1;
                }
                int num = await menuService.InsertMenuAsync(menuto);
                if (num > 0)
                {
                    result.Message = "新增成功!";
                    result.Status = 200;
                    await redisClient.DeleteHashOfAsync("WMS_MenuList", "menuhash");
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
                var menuto = await menuService.GetMenuByIdAsync(model.Id);
                menuto.Name = model.Name;
                menuto.Title = model.Title;
                menuto.NavigateController = model.NavigateController;
                menuto.NavigateActioin = model.NavigateActioin;
                menuto.Tag = model.Tag;
                menuto.ParentName = model.ParentName;
                menuto.HasChildren = model.HasChildren;
                menuto.HeadStyle = model.HeadStyle;
                menuto.Style = model.Style;
                menuto.Url = model.Url;
                menuto.UpdateTime = DateTime.Now.ToString();
                var menu= mapper.Map<MenuDto>(menuto);
                int num = await menuService.UpdateMenuAsync(menu);
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
                int num = await menuService.DeleteMenuAsync(id);
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
