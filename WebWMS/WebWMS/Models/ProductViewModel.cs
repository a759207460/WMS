using WebWMS.Core.Domain.Products;

namespace WebWMS.Models
{
    public class ProductViewModel
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int Id { get; set; }

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
    }
}
