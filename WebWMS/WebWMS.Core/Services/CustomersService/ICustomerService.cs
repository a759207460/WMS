using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.CustomersService
{
    public interface ICustomerService
    {
        public Task<CustomerDto> GetCustomerByIdAsync(int id);

        public Task<bool> GetCustomerByNameAsync(string code, string name);

        public Task<List<CustomerDto>> GetAllAsync();
        public Task<IPagedList<CustomerDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where);

        public Task<int> InsertCustomerAsync(CustomerDto customerDto);

        public Task<int> UpdateCustomerAsync(CustomerDto customerDto);

        public Task<int> DeleteCustomerAsync(int id);

        public Task<Dictionary<bool, string?>> CustomerAnyAsync(List<CustomerDto> list);

        public Task<int> BatchInsertCustomerAsync(List<CustomerDto> listCustomerDto, CancellationToken cancellationToken);

        public Task<List<ExportCustomerDto>> GetExportAsync();
    }
}
