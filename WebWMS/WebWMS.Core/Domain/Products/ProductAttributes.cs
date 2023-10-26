using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Products
{
    public class ProductAttributes : BaseModel
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 产品明细编号
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// 产品单价
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 产品重量
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 产品成本
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 产品长度
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// 产品宽度
        /// </summary>
        public double Wide { get; set; }

        /// <summary>
        /// 产品高度
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 产品体积
        /// </summary>
        public double Volume { get; set; }
    }
}
