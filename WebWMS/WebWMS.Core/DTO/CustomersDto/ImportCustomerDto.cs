using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.CustomersDto
{
    public class ImportCustomerDto
    {
        public string 客户编号 { get; set; }

        public string 客户名称 { get; set; }

        public string 所在城市 { get; set; }
        public string 详细地址 { get; set; }

        public string 负责人 { get; set; }
        public string 联系方式 { get; set; }
    }
}
