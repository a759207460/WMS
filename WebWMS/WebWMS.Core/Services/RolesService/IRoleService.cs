using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.RolesService
{
    public interface IRoleService
    {
        public Task<List<RoleDto>> GetAllAsync();
        public Task<IPagedList<RoleDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where);

        Task<bool> GetRoleByNameAsync(string code, string name);
        public Task<int> InsertRoleAsync(RoleDto roleDto);

        public Task<int> UpdateRoleAsync(RoleDto roleDto);

        public Task<int> DeleteRoleAsync(int id);
    }
}
