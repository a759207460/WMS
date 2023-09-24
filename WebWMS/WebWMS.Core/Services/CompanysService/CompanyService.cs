using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Repositorys;
using WebWMS.Core.DTO.Companys;
using WebWMS.Core.Domain.Companys;
using WebWMS.Core.DTO.UserInfosDto;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.DTO.CompanysDto;

namespace WebWMS.Core.Services.CompanysService
{
    public class CompanyService:ICompanyService
    {
        private readonly IRepository<Company> repository;
        private readonly IMapper mapper;

        public CompanyService(IRepository<Company> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取所有公司信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<CompanyDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            var menuList = mapper.Map<List<CompanyDto>>(list);
            return menuList;
        }

        /// <summary>
        /// 获取公司列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IPagedList<CompanyDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<CompanyDto> pagedList = null;
            IPagedList<Company> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: r => r.CompanyName.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<CompanyDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }

        /// <summary>
        /// 根据id获取公司
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompanyDto> GetCompanyByIdAsync(int id)
        {
            var cu = await repository.FindAsync(id);
            var cuto = mapper.Map<CompanyDto>(cu);
            return cuto;
        }

        /// <summary>
        /// 根据菜单名称获取公司
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> GetCompanyByNameAsync(string code,string name)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.CompanyCode == code||r.CompanyName== name);
        }


        /// <summary>
        /// 获取所有用户集合
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<bool, string?>>CompanyAnyAsync(List<CompanyDto> list)
        {
            Dictionary<bool, string?> dic = new Dictionary<bool, string?>();
            List<string> cu = list.Select(c => c.CompanyCode).ToList();
            List<string> clist = (await repository.GetAllAsync()).Select(cu => cu.CompanyCode).ToList();
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
        public async Task<int> BatchInsertCompanyAsync(List<CompanyDto> listCustomerDto, CancellationToken cancellationToken)
        {
            var cp = mapper.Map<List<Company>>(listCustomerDto);
            return await repository.InsertAsync(cp, cancellationToken);
        }


        /// <summary>
        /// 导出客户信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ExportCompanyDto>> GetExportAsync()
        {
            var list = await repository.GetAllAsync();
            var nlist = list.Select(c => new ExportCompanyDto { CompanyCode = c.CompanyCode, CompanyName = c.CompanyName, CompanyCity = c.CompanyCity, CompanyAddress = c.CompanyAddress, CompanyPrincipal = c.CompanyPrincipal, CompanyContact=c.CompanyContact });
            return mapper.Map<List<ExportCompanyDto>>(nlist);
        }


        /// <summary>
        /// 新增公司
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> InsertCompanyAsync(CompanyDto companyDto)
        {
            var cu = mapper.Map<Company>(companyDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 更新公司信息
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateCompanyAsync(CompanyDto companyDto)
        {
            var company = await repository.FindAsync(companyDto.Id);
            if (company != null)
            {
                company.CompanyCode= companyDto.CompanyCode;
                company.CompanyName= companyDto.CompanyName;
                company.CompanyAddress= companyDto.CompanyAddress;
                company.CompanyCity= companyDto.CompanyCity;
                company.CompanyPrincipal= companyDto.CompanyPrincipal;
                company.IsDelete=companyDto.IsDelete;
                company.UpdateTime=companyDto.UpdateTime;
                return await repository.Update(company);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteCompanyAsync(int id)
        {
            return await repository.Delete(id);
        }
    }
}
