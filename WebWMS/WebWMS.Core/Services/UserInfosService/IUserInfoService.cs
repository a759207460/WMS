using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.UserInfosService
{
    public interface IUserInfoService
    {
        public Task<UserInfoDto> GetCustomerByIdAsync(int id);

        public Task<bool> GetCustomerByAccountAsync(string account);

        public Task<IPagedList<UserInfoDto>> GetAllPageListAsync(int pageIndex, int pageSize, string where);

        public  Task<List<UserInfoDto>> GetAllAsync();

        public Task<List<ExportUserInfDto>> GetExportAsync();

        public Task<Dictionary<bool, string?>> CustomeAnyAsync(List<UserInfoDto> list);

        public Task<UserInfoDto> GetCustomerAsync(string account, string pwd);

        public Task<int> InsertCustomerAsync(UserInfoDto customerDto);

        Task<int> BatchInsertCustomerAsync(List<UserInfoDto> listCustomerDto, CancellationToken cancellationToken);

        public Task<int> UpdateCustomerAsync(UserInfoDto customerDto);

        public Task<int> DeleteCustomerAsync(int id);
    }
}
