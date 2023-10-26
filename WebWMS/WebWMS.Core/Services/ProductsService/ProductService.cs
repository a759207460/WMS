using AutoMapper;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core.DAG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Products;
using WebWMS.Core.Domain.Roles;
using WebWMS.Core.DTO.ProductsDto;
using WebWMS.Core.DTO.RolesDto;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.Collections;

namespace WebWMS.Core.Services.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> repository;
        private readonly IMapper mapper;

        public ProductService(IRepository<Product> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            var productList = mapper.Map<List<ProductDto>>(list);
            return productList;
        }

        public async Task<IPagedList<ProductDto>> GetAllPagedListAsync(int pageIndex, int pageSize, string where)
        {
            PagedList<ProductDto> pagedList = null;
            IPagedList<Product> plist = null;
            if (!string.IsNullOrWhiteSpace(where))
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize, predicate: r => r.ProductName.Contains(where));
            else
                plist = await repository.GetPagedListAsync(pageIndex: pageIndex, pageSize: pageSize);
            if (plist != null)
            {
                var list = mapper.Map<PagedList<ProductDto>>(plist);
                pagedList = list;
            }
            return pagedList;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertProductAsync(ProductDto roleDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateProductAsync(ProductDto roleDto)
        {
            throw new NotImplementedException();
        }
    }
}
