using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.VendorsDto
{
    public class ExportVendorDto
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string VendorCode { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string VendorName { get; set; }

        /// <summary>
        /// 供应商所在城市
        /// </summary>
        public string VendorCity { get; set; }

        /// <summary>
        /// 供应商地址
        /// </summary>
        public string VendorAddress { get; set; }

        /// <summary>
        ///供应商负责人
        /// </summary>
        public string VendorPrincipal { get; set; }

        /// <summary>
        ///供应商联系方式
        /// </summary>
        public string VendorContact { get; set; }
    }
}
