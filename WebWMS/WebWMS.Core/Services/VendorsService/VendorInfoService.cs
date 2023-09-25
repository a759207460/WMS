using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Companys;
using WebWMS.Core.DTO.Companys;
using WebWMS.Core.DTO.CompanysDto;
using WebWMS.Core.Repositorys.Collections;
using WebWMS.Core.Repositorys;
using System.Numerics;
using WebWMS.Core.Domain.Vendors;
using WebWMS.Core.DTO.VendorsDto;

namespace WebWMS.Core.Services.VendorsService
{
    public class VendorInfoService:IVendorInfoService
    {
        private readonly IRepository<VendorInfo> repository;
        private readonly IMapper mapper;

        public VendorInfoService(IRepository<VendorInfo> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取所有供应商信息
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<VendorInfoDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            var vendorList = mapper.Map<List<VendorInfoDto>>(list);
            return vendorList;
        }

        /// <summary>
        /// 获取供应商列表分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public async Task<IPagedList<VendorInfoDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<VendorInfoDto> pagedList = null;
            IPagedList<VendorInfo> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: r => r.VendorName.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<VendorInfoDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }

        /// <summary>
        /// 根据id获取供应商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VendorInfoDto> GetVendorByIdAsync(int id)
        {
            var cu = await repository.FindAsync(id);
            var cuto = mapper.Map<VendorInfoDto>(cu);
            return cuto;
        }

        /// <summary>
        /// 根据供应商名称编号获取供应商
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> GetVendorByNameAsync(string code, string name)
        {
            if (string.IsNullOrEmpty(code))
                return false;
            return await repository.GetFirstOrDefaultAsync<bool>(r => r.VendorCode == code || r.VendorName == name);
        }


        /// <summary>
        /// 获取所有供应商集合
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<bool, string?>> VendorAnyAsync(List<VendorInfoDto> list)
        {
            Dictionary<bool, string?> dic = new Dictionary<bool, string?>();
            List<string> cu = list.Select(c => c.VendorCode).ToList();
            List<string> clist = (await repository.GetAllAsync()).Select(cu => cu.VendorCode).ToList();
            bool b = clist.Intersect(cu).Count() > 0;
            string? code = string.Join(",", clist?.Intersect(cu).ToArray());
            dic.Add(b, code);
            return dic;
        }


        /// <summary>
        /// 批量新增供应商
        /// </summary>
        /// <param name="customerDto"></param>
        /// <returns></returns>
        public async Task<int> BatchInsertVendorAsync(List<VendorInfoDto> listCustomerDto, CancellationToken cancellationToken)
        {
            var cp = mapper.Map<List<VendorInfo>>(listCustomerDto);
            return await repository.InsertAsync(cp, cancellationToken);
        }


        /// <summary>
        /// 导出供应商信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<ExportVendorDto>> GetExportAsync()
        {
            var list = await repository.GetAllAsync();
            var nlist = list.Select(c => new ExportVendorDto { VendorCode = c.VendorCode, VendorName = c.VendorName, VendorCity = c.VendorCity, VendorAddress = c.VendorAddress, VendorPrincipal = c.VendorPrincipal, VendorContact = c.VendorContact });
            return mapper.Map<List<ExportVendorDto>>(nlist);
        }


        /// <summary>
        /// 新增供应商
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        public async Task<int> InsertVendorAsync(VendorInfoDto companyDto)
        {
            var cu = mapper.Map<VendorInfo>(companyDto);
            return await repository.InsertAsync(cu);
        }

        /// <summary>
        /// 更新供应商信息
        /// </summary>
        /// <param name="menuDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> UpdateVendorAsync(VendorInfoDto vendorDto)
        {
            var vendor = await repository.FindAsync(vendorDto.Id);
            if (vendor != null)
            {
                vendor.VendorCode = vendorDto.VendorCode;
                vendor.VendorName = vendorDto.VendorName;
                vendor.VendorAddress = vendorDto.VendorAddress;
                vendor.VendorCity = vendorDto.VendorCity;
                vendor.VendorPrincipal = vendorDto.VendorPrincipal;
                vendor.VendorContact = vendorDto.VendorContact;
                vendor.UpdateTime = vendorDto.UpdateTime;
                return await repository.Update(vendor);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除供应商信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> DeleteVendorAsync(int id)
        {
            return await repository.Delete(id);
        }
    }
}
