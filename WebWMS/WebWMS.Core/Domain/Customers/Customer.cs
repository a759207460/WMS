using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Customers
{
    public class Customer:BaseModel
    {
        /// <summary>
        /// 客户编号
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户所在城市
        /// </summary>
        public string CustomerCity { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerAddress { get; set; }

        /// <summary>
        ///客户负责人
        /// </summary>
        public string CustomerPrincipal { get; set; }

        /// <summary>
        ///客户联系方式
        /// </summary>
        public string CustomerContact { get; set; }
    }
}
