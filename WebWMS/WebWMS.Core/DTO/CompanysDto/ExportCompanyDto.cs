using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.CompanysDto
{
    public class ExportCompanyDto
    {
        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司所在城市
        /// </summary>
        public string CompanyCity { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        ///公司负责人
        /// </summary>
        public string CompanyPrincipal { get; set; }

        /// <summary>
        ///公司联系方式
        /// </summary>
        public string CompanyContact { get; set; } 
    }
}
