using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.VendorsDto
{
    public class ImportVendorDto
    {
        public string 供应商编号 { get; set; }

        public string 供应商名称 { get; set; }

        public string 所在城市 { get; set; }
        public string 详细地址 { get; set; }

        public string 负责人 { get; set; }
        public string 联系方式 { get; set; }
    }
}
