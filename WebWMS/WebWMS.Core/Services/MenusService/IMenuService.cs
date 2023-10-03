using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.MenusService
{
    public interface IMenuService
    {
        public Task<MenuDto> GetMenuByIdAsync(int id);
        public Task<bool> GetMenuByNameAsync(string name, string title);
        public Task<List<MenuDto>> GetAllAsync();
        public Task<List<MenuDto>> GetMenusByIdsAsync(List<int> ids);
        public Task<IPagedList<MenuDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where);
        public Task<int> InsertMenuAsync(MenuDto menuDto);
        public Task<int> UpdateMenuAsync(MenuDto menuDto);
        public Task<int> DeleteMenuAsync(int id);
    }
}
