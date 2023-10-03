using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.Repositorys;

namespace WebWMS.Core.Services.MenusService
{
    public class MenuRoleInfoService : IMenuRoleInfoService
    {
        //private readonly IRepository<MenuRoleInfo> repository;
        //private readonly IMapper mapper;

        //public MenuRoleInfoService(IRepository<MenuRoleInfo> repository, IMapper mapper)
        //{
        //    this.repository = repository;
        //    this.mapper = mapper;
        //}

        /// <summary>
        /// 根据菜单id获取权限数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //public async Task<List<MenuRoleInfo>> GetMenuRoleInfoByIds(List<int> list)
        //{
        //    var mlist = await repository.GetAllAsync(predicate: m => list.Contains(m.MenusId));
        //    return mlist.ToList();
        //}

        /// <summary>
        /// 批量删除权限数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        //public async Task<int> RemoveMenuRoleInfo(List<MenuRoleInfo> list)
        //{
        //    int num= await repository.Delete(list);
        //    return num;
        //}
    }
}
