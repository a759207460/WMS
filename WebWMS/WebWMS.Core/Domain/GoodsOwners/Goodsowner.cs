using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.GoodsOwners
{
    public class Goodsowner:BaseModel
    {
        #region Property
        /// <summary>
        /// goods owner name
        /// </summary>
        public string goods_owner_name { get; set; }

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
        /// creator
        /// </summary>
        public string creator { get; set; }
         
 
        /// <summary>
        /// valid
        /// </summary>
        public bool is_valid { get; set; }

        /// <summary>
        /// the tenant id
        /// </summary>
        public long tenant_id { get; set; }
        #endregion
    }
}
