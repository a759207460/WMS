using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Products;
using WebWMS.Core.DTO.ProductsDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.ProductsService
{
    public interface IProductService
    {
        public Task<List<ProductDto>> GetAllAsync();
        public Task<ProductDto> GetProductByIdAsync(int id);
        public Task<IPagedList<ProductDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where);
        public Task<int> InsertProductAsync(ProductDto roleDto);
        public Task<int> UpdateProductAsync(ProductDto roleDto);
        public Task<int> DeleteProductAsync(int id);
    }
}
