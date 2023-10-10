using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.MenusService
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> repository;
        private readonly IMapper mapper;

        public MenuService(IRepository<Menu> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取所有菜单信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<MenuDto>> GetAllAsync()
        {
            var list = (await repository.GetAllAsync()).OrderBy(m => m.Sort).ToList();
            var menuList = mapper.Map<List<MenuDto>>(list);
            return menuList;
        }

        /// <summary>
        ///根据多个id获取菜单信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenusByIdsAsync(List<int> ids)
        {
            var list = await repository.GetAllAsync(predicate:m=>ids.Contains(m.Id));
            var menuList = mapper.Map<List<MenuDto>>(list);
            return menuList;
        }


        /// <summary>
        ///根据多个角色id获取菜单信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenusByRoleIdsAsync(List<int> ids)
        {
            var list = await repository.GetAllAsync(predicate: m =>m.Roles.Any(r=>ids.Contains(r.RoleId)),include:m1=>m1.Include(mm=>mm.Roles),orderBy:rr=>rr.OrderBy(s=>s.Sort));
            var menuList = mapper.Map<List<MenuDto>>(list);
            return menuList;
        }


        /// <summary>
        /// 获取菜单列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IPagedList<MenuDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<MenuDto> pagedList = null;
            IPagedList<Menu> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: r => r.Title.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<MenuDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }

        /// <summary>
        /// 根据id获取菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MenuDto> GetMenuByIdAsync(int id)
        {
            var cu = await repository.FindAsync(id);
            var cuto = mapper.Map<MenuDto>(cu);
            return cuto;
        }

        /// <summary>
        /// 根据菜单名称获取菜单
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> GetMenuByNameAsync(string name, string title)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(title))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.Name == name);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> InsertMenuAsync(MenuDto menuDto)
        {
            var cu = mapper.Map<Menu>(menuDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateMenuAsync(MenuDto menuDto)
        {
            var menu = await repository.FindAsync(menuDto.Id);
            if (menu != null)
            {
                menu.Name = menuDto.Name;
                menu.Title = menuDto.Title;
                menu.NavigateController = menuDto.NavigateController;
                menu.NavigateActioin = menuDto.NavigateActioin;
                menu.ParentName = menuDto.ParentName;
                menu.HasChildren = menuDto.HasChildren;
                menu.UpdateTime = menuDto.UpdateTime;
                menu.Tag = menuDto.Tag;
                menu.Style = menuDto.Style;
                menu.HeadStyle = menuDto.HeadStyle;
                menu.Url= menuDto.Url;
                return await repository.Update(menu);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteMenuAsync(int id)
        {
            return await repository.Delete(id);
        }


    }
}
