using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.DTO.Customers;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.CustomersService
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> repository;
        private readonly IMapper mapper;

        public CustomerService(IRepository<Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IPagedList<CustomerDto>> GetAllPageListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<CustomerDto> pagedList = null;
            IPagedList<Customer> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: c => c.Account.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<CustomerDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }


        public async Task<List<CustomerDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            return mapper.Map<List<CustomerDto>>(list);
        }
        /// <summary>
        /// 获取所有用户集合
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<bool, string?>> CustomeAnyAsync(List<CustomerDto> list)
        {
            Dictionary<bool, string?> dic = new Dictionary<bool, string?>();
            List<string> cu = list.Select(c => c.Account).ToList();
            List<string> clist = (await repository.GetAllAsync()).Select(cu => cu.Account).ToList();
            bool b = clist.Intersect(cu).Count() > 0;
            string? account = string.Join(",", clist?.Intersect(cu).ToArray());
            dic.Add(b, account);
            return dic;
        }
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var cu = await repository.FindAsync(id);
            var cuto = mapper.Map<CustomerDto>(cu);
            return cuto;
        }

        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<bool> GetCustomerByAccountAsync(string account)
        {
            if (string.IsNullOrEmpty(account))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.Account == account);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        public async Task<int> InsertCustomerAsync(CustomerDto customerDto)
        {
            var cu = mapper.Map<Customer>(customerDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        public async Task<int> BatchInsertCustomerAsync(List<CustomerDto> listCustomerDto, CancellationToken cancellationToken)
        {
            var cu = mapper.Map<List<Customer>>(listCustomerDto);
            return await repository.InsertAsync(cu, cancellationToken);
        }



        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="customerDto"></param>
        public async Task<int> UpdateCustomerAsync(CustomerDto customerDto)
        {
            var cu = await repository.FindAsync(customerDto.Id);
            if (cu != null)
            {
                cu.Address = customerDto.Address;
                cu.Email = customerDto.Email;
                cu.IsRemove = customerDto.IsRemove;
                cu.IsEnabled = customerDto.IsEnabled;
                cu.MoblePhone = customerDto.MoblePhone;
                cu.Name = customerDto.Name;
                cu.UpdateTime = customerDto.UpdateTime;
                return await repository.Update(cu);
            }
            else
            {
                return -1;
            }

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteCustomerAsync(int id)
        {
            return await repository.Delete(id);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<bool> GetCustomerAsync(string account, string pwd)
        {
            var cu = await repository.GetFirstOrDefaultAsync<int>(c => c.Id, predicate: c => c.Account == account && c.PassWord == pwd && c.IsEnabled && !c.IsRemove);
            return cu > 0;
        }
    }
}
