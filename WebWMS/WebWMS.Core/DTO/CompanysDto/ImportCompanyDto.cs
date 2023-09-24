using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.CompanysDto
{
    public class ImportCompanyDto
    {
        public string 公司编号 { get; set; }

        public string 公司名称 { get; set; }

        public string 所在城市 { get; set; }
        public string 详细地址 { get; set; }

        public string 负责人 { get; set; }
        public string 联系方式 { get; set; }
    }
}
