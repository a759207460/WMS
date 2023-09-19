using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.DTO.Customers;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.CustomersService
{
    public interface ICustomerService
    {
        public Task<CustomerDto> GetCustomerByIdAsync(int id);

        public Task<bool> GetCustomerByAccountAsync(string account);

        public Task<IPagedList<CustomerDto>> GetAllPageListAsync(int pageIndex, int pageSize, string where);

        public  Task<List<CustomerDto>> GetAllAsync();

        public Task<Dictionary<bool, string?>> CustomeAnyAsync(List<CustomerDto> list);

        public Task<bool> GetCustomerAsync(string account, string pwd);

        public Task<int> InsertCustomerAsync(CustomerDto customerDto);

        Task<int> BatchInsertCustomerAsync(List<CustomerDto> listCustomerDto, CancellationToken cancellationToken);

        public Task<int> UpdateCustomerAsync(CustomerDto customerDto);

        public Task<int> DeleteCustomerAsync(int id);
    }
}
