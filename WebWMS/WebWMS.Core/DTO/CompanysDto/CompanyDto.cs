using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.Companys
{
    public class CompanyDto:BaseDto
    {
        /// <summary>
        /// company's Name
        /// </summary>
        public string company_name { get; set; } = string.Empty;

        /// <summary>
        /// city
        /// </summary>
        public string city { get; set; } = string.Empty;

        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; } = string.Empty;

        /// <summary>
        /// manager
        /// </summary>
        public string manager { get; set; } = string.Empty;

        /// <summary>
        /// contact tel
        /// </summary>
        public string contact_tel { get; set; } = string.Empty;

  
        /// <summary>
        /// the tenant id
        /// </summary>
        public long tenant_id { get; set; } = 1;
    }
}
