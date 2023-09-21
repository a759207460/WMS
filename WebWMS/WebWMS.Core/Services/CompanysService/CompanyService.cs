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
        public async Task<bool> GetCompanyByNameAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.CompanyCode == code);
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
