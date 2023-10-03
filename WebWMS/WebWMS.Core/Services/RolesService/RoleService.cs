using AutoMapper;
using CommonLibraries.DeepCopy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.Domain.Roles;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.MenusService;
using static CommonLibraries.DeepCopy.DeepCoypHelp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebWMS.Core.Services.RolesService
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleInfo> repository;
        private readonly IMapper mapper;
        private readonly IMenuRoleInfoService menuRoleInfoService;

        public RoleService(IRepository<RoleInfo> repository, IMapper mapper, IMenuRoleInfoService menuRoleInfoService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.menuRoleInfoService = menuRoleInfoService;
        }


        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<RoleDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            var roleList = mapper.Map<List<RoleDto>>(list);
            return roleList;
        }
        ///// <summary>
        /////  根据id获取所有角色信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        public async Task<RoleDto> GetRoleByIdAsync(int id)
        {
            var r = await repository.GetFirstOrDefaultAsync(predicate: r => r.Id == id, include: r1 => r1.Include(rr => rr.Menus));
            var role = mapper.Map<RoleDto>(r);
            return role;
        }


        /// <summary>
        /// 根据角色名称获取角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> GetRoleByNameAsync(string code, string name)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.RoleCode == code || r.RoleName == name);
        }


        /// <summary>
        /// 获取角色列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IPagedList<RoleDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<RoleDto> pagedList = null;
            IPagedList<RoleInfo> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: r => r.RoleName.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<RoleDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }



        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> InsertRoleAsync(RoleDto roleDto)
        {
            var cu = mapper.Map<RoleInfo>(roleDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateRoleAsync(RoleDto roleDto)
        {
            using (IDbContextTransaction dbContextTransaction = repository.db.Database.BeginTransaction())
            {
                try
                {
                    var roleInfo = await repository.GetFirstOrDefaultAsync(include: r => r.Include(r1 => r1.Menus), predicate: r => r.Id == roleDto.Id);
                    var role = mapper.Map<RoleDto>(roleInfo);
                    if (role != null)
                    {
                        var mlist =roleDto.Menus;
                        repository.db.MenuAndRoles.RemoveRange(roleInfo.Menus);
                        await repository.db.SaveChangesAsync();
                        roleInfo.RoleCode = roleDto.RoleCode;
                        roleInfo.RoleName = roleDto.RoleName;
                        roleInfo.Menus = mlist;
                        roleInfo.UpdateTime = roleDto.UpdateTime;
                        int n = await repository.Update(roleInfo);
                        await dbContextTransaction.CommitAsync();
                        return n;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    await dbContextTransaction.RollbackAsync();
                    return -1;
                }
            }
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteRoleAsync(int id)
        {
            return await repository.Delete(id);
        }

    }
}
