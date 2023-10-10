using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Services.RolesService;
using WebWMS.Core.Services.UserInfosService;

namespace WebWMS.Core.Services.UserInfosService
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IRepository<UserInfo> repository;
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public UserInfoService(IRepository<UserInfo> repository, IRoleService roleService, IMapper mapper)
        {
            this.repository = repository;
            this.roleService = roleService;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取用户列表信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IPagedList<UserInfoDto>> GetAllPageListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<UserInfoDto> pagedList = null;
            IPagedList<UserInfo> plist = null;
            var rlist = await roleService.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: c => c.Account.Contains(where), include: u => u.Include(r => r.Roles));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, include: u => u.Include(r => r.Roles));
            if (plist != null)
            {
                var list = mapper.Map<PagedList<UserInfoDto>>(plist);
                List<string> nlist = null;
                List<int> nlistId = null;
                for (int i = 0; i < list.Items.Count; i++)
                {
                    nlist = rlist.Where(r => list.Items[i].Roles.Where(rr => rr.RoleId == r.Id).Count() > 0).Select(r1 => r1.RoleName).ToList();
                    nlistId = rlist.Where(r => list.Items[i].Roles.Where(rr => rr.RoleId == r.Id).Count() > 0).Select(r1 => r1.Id).ToList();
                    if (nlist != null && nlist.Count() > 0)
                        list.Items[i].RoleNames = string.Join(",", nlist);
                    list.Items[i].RoleIds = nlistId;
                }
                pagedList = list;
            }
            return pagedList;
        }


        public async Task<List<UserInfoDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            return mapper.Map<List<UserInfoDto>>(list);
        }

        /// <summary>
        /// 导出客户信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ExportUserInfDto>> GetExportAsync()
        {
            var list = await repository.GetAllAsync();
            var nlist = list.Select(c => new ExportUserInfDto { Account = c.Account, Name = c.Name, MoblePhone = c.MoblePhone, Email = c.Email, Address = c.Address, IsEnabled = (c.IsEnabled == true ? "是" : "否"), IsRemove = (c.IsRemove == true ? "是" : "否") });
            return mapper.Map<List<ExportUserInfDto>>(nlist);
        }


        /// <summary>
        /// 获取所有用户集合
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<bool, string?>> CustomeAnyAsync(List<UserInfoDto> list)
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
        public async Task<UserInfoDto> GetCustomerByIdAsync(int id)
        {
            var cu = await repository.GetFirstOrDefaultAsync(predicate: u => u.Id == id, include: u1 => u1.Include(r => r.Roles));
            var cuto = mapper.Map<UserInfoDto>(cu);
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
        public async Task<int> InsertCustomerAsync(UserInfoDto userDto)
        {

            var cu = mapper.Map<UserInfo>(userDto);
            return await repository.InsertAsync(cu);
        }



        /// <summary>
        /// 批量新增用户
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        public async Task<int> BatchInsertCustomerAsync(List<UserInfoDto> listCustomerDto, CancellationToken cancellationToken)
        {
            var cu = mapper.Map<List<UserInfo>>(listCustomerDto);
            return await repository.InsertAsync(cu, cancellationToken);
        }



        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="customerDto"></param>
        public async Task<int> UpdateCustomerAsync(UserInfoDto customerDto)
        {
            using (IDbContextTransaction dbContextTransaction = repository.db.Database.BeginTransaction())
            {
                try
                {
                    var cu = await repository.GetFirstOrDefaultAsync(predicate: u => u.Id == customerDto.Id, include: u => u.Include(u1 => u1.Roles));
                    if (cu != null)
                    {
                        repository.db.UserAndRoles.RemoveRange(cu.Roles);
                        await repository.db.SaveChangesAsync();
                        cu.Address = customerDto.Address;
                        cu.Email = customerDto.Email;
                        cu.IsRemove = customerDto.IsRemove;
                        cu.IsEnabled = customerDto.IsEnabled;
                        cu.MoblePhone = customerDto.MoblePhone;
                        cu.Name = customerDto.Name;
                        cu.Roles =mapper.Map<List<UserAndRoles>>(customerDto.Roles);
                        cu.UpdateTime = customerDto.UpdateTime;
                        int n= await repository.Update(cu);
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
                    throw new Exception(ex.Message);
                }

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
        public async Task<UserInfoDto> GetCustomerAsync(string account, string pwd)
        {
            var cu = await repository.GetFirstOrDefaultAsync(c => c, predicate: c => c.Account == account && c.PassWord == pwd && c.IsEnabled && !c.IsRemove);
            var customer = mapper.Map<UserInfoDto>(cu);
            return customer;
        }
    }
}
