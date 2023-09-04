using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.suppliers
{
    public class supplier:BaseModel
    {
        #region Property

        /// <summary>
        /// supplier_name
        /// </summary>
        public string supplier_name { get; set; } 

        /// <summary>
        /// city
        /// </summary>
        public string city { get; set; } 

        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// manager
        /// </summary>
        public string manager { get; set; }

        /// <summary>
        /// contact_tel
        /// </summary>
        public string contact_tel { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } 

        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } 

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } 


        #endregion
    }
}
