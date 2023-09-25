using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.DTO.CustomersDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Repositorys;

namespace WebWMS.Core.Services.CustomersService
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> repository;
        private readonly IMapper mapper;

        public CustomerService(IRepository<Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取所有客户信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            var menuList = mapper.Map<List<CustomerDto>>(list);
            return menuList;
        }

        /// <summary>
        /// 获取客户列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IPagedList<CustomerDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<CustomerDto> pagedList = null;
            IPagedList<Customer> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: r => r.CustomerName.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<CustomerDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }

        /// <summary>
        /// 根据id获取客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var cu = await repository.FindAsync(id);
            var cuto = mapper.Map<CustomerDto>(cu);
            return cuto;
        }

        /// <summary>
        /// 根据菜单名称获取客户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> GetCustomerByNameAsync(string code, string name)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.CustomerCode == code || r.CustomerName == name);
        }


        /// <summary>
        /// 获取所有用户集合
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<bool, string?>> CustomerAnyAsync(List<CustomerDto> list)
        {
            Dictionary<bool, string?> dic = new Dictionary<bool, string?>();
            List<string> cu = list.Select(c => c.CustomerCode).ToList();
            List<string> clist = (await repository.GetAllAsync()).Select(cu => cu.CustomerCode).ToList();
            bool b = clist.Intersect(cu).Count() > 0;
            string? code = string.Join(",", clist?.Intersect(cu).ToArray());
            dic.Add(b, code);
            return dic;
        }


        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        public async Task<int> BatchInsertCustomerAsync(List<CustomerDto> listCustomerDto, CancellationToken cancellationToken)
        {
            var cp = mapper.Map<List<Customer>>(listCustomerDto);
            return await repository.InsertAsync(cp, cancellationToken);
        }


        /// <summary>
        /// 导出客户信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ExportCustomerDto>> GetExportAsync()
        {
            var list = await repository.GetAllAsync();
            var nlist = list.Select(c => new ExportCustomerDto { CustomerCode = c.CustomerCode, CustomerName = c.CustomerName, CustomerCity = c.CustomerCity, CustomerAddress = c.CustomerAddress, CustomerPrincipal = c.CustomerPrincipal, CustomerContact = c.CustomerContact });
            return mapper.Map<List<ExportCustomerDto>>(nlist);
        }


        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> InsertCustomerAsync(CustomerDto CustomerDto)
        {
            var cu = mapper.Map<Customer>(CustomerDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateCustomerAsync(CustomerDto CustomerDto)
        {
            var Customer = await repository.FindAsync(CustomerDto.Id);
            if (Customer != null)
            {
                Customer.CustomerCode = CustomerDto.CustomerCode;
                Customer.CustomerName = CustomerDto.CustomerName;
                Customer.CustomerAddress = CustomerDto.CustomerAddress;
                Customer.CustomerCity = CustomerDto.CustomerCity;
                Customer.CustomerPrincipal = CustomerDto.CustomerPrincipal;
                Customer.CustomerContact = CustomerDto.CustomerContact;
                Customer.UpdateTime = CustomerDto.UpdateTime;
                return await repository.Update(Customer);
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
        public async Task<int> DeleteCustomerAsync(int id)
        {
            return await repository.Delete(id);
        }
    }
}
