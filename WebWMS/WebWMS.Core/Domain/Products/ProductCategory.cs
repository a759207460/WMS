using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Products
{
    public class ProductCategory:BaseModel
    {
        /// <summary>
        /// 分类编号
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 父类Id
        /// </summary>
        public int ParentCategoryId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

    }
}
