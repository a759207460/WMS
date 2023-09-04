using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.GoodsOwners
{
    public class GoodsownerDto: BaseDto
    {
        #region Property
        /// <summary>
        /// goods owner name
        /// </summary>
        public string goods_owner_name { get; set; } = string.Empty;

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
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;
         
 
        /// <summary>
        /// valid
        /// </summary>
        public bool is_valid { get; set; } = true;

        /// <summary>
        /// the tenant id
        /// </summary>
        public long tenant_id { get; set; } = 1;
        #endregion
    }
}
