﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.Companys;
using WebWMS.Core.DTO.MenusDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.CompanysService
{
    public interface ICompanyService
    {
        public Task<CompanyDto> GetCompanyByIdAsync(int id);

        public Task<bool> GetCompanyByNameAsync(string code);

        public Task<List<CompanyDto>> GetAllAsync();
        public Task<IPagedList<CompanyDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where);

        public Task<int> InsertCompanyAsync(CompanyDto companyDto);

        public Task<int> UpdateCompanyAsync(CompanyDto companyDto);

        public Task<int> DeleteCompanyAsync(int id);
    }
}
