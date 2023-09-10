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

        public Task<IPagedList<CustomerDto>> GetAllAsync(int pageIndex, int pageSize, string where);

        public Task<bool> GetCustomerAsync(string account, string pwd);

        public Task<int> InsertCustomerAsync(CustomerDto customerDto);

        public Task<int> UpdateCustomerAsync(CustomerDto customerDto);

        public Task<int> DeleteCustomerAsync(int id);
    }
}
