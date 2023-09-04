using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Companys
{
    public class Company:BaseModel
    {
        /// <summary>
        /// company's Name
        /// </summary>
        public string company_name { get; set; }

        /// <summary>
        /// city
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// manager
        /// </summary>
        public string manager { get; set; }

        /// <summary>
        /// contact tel
        /// </summary>
        public string contact_tel { get; set; }

  
        /// <summary>
        /// the tenant id
        /// </summary>
        public long tenant_id { get; set; }
    }
}
