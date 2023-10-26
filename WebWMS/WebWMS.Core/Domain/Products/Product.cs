using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Products
{
    public class Product : BaseModel
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProductDescription { get; set; }

        /// <summary>
        /// 产品条码
        /// </summary>
        public string ProductBarcode { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public int VendorId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 产品明细
        /// </summary>
        public ICollection<ProductAttributes> ProductAttributes { get; set; }

        /// <summary>
        /// 产品分类
        /// </summary>
        public ProductCategory ProductCategory { get; set; }
    }
}
