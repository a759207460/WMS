using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.VendorsDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.VendorsService
{
    public interface IVendorInfoService
    {
        public Task<VendorInfoDto> GetVendorByIdAsync(int id);

        public Task<bool> GetVendorByNameAsync(string code, string name);

        public Task<List<VendorInfoDto>> GetAllAsync();
        public Task<IPagedList<VendorInfoDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where);

        public Task<int> InsertVendorAsync(VendorInfoDto vendorInfoDto);

        public Task<int> UpdateVendorAsync(VendorInfoDto vendorInfoDto);

        public Task<int> DeleteVendorAsync(int id);

        public Task<Dictionary<bool, string?>> VendorAnyAsync(List<VendorInfoDto> list);

        public Task<int> BatchInsertVendorAsync(List<VendorInfoDto> listVendorInfoDto, CancellationToken cancellationToken);

        public Task<List<ExportVendorDto>> GetExportAsync();
    }
}
