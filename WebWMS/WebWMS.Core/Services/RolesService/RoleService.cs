using AutoMapper;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Roles;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.RolesService
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<RoleInfo> repository;
        private readonly IMapper mapper;

        public RoleService(IRepository<RoleInfo> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }


        /// <summary>
        /// 获取所有客户信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<RoleDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            var menuList = mapper.Map<List<RoleDto>>(list);
            return menuList;
        }


        /// <summary>
        /// 根据菜单名称获取客户
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
        /// 获取客户列表分页
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
        /// 新增客户
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> InsertRoleAsync(RoleDto roleDto)
        {
            var cu = mapper.Map<RoleInfo>(roleDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateRoleAsync(RoleDto roleDto)
        {
            var role = await repository.FindAsync(roleDto.Id);
            if (role != null)
            {
                role.RoleCode = roleDto.RoleCode;
                role.RoleName = roleDto.RoleName;
                role.UpdateTime = roleDto.UpdateTime;
                return await repository.Update(role);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除客户信息
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
