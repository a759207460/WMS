using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stockadjusts
{
    public class Stockadjust : BaseModel
    {
        #region Property

        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// goods_owner_id
        /// </summary>
        public int goods_owner_id { get; set; } 

        /// <summary>
        /// goods_location_id
        /// </summary>
        public int goods_location_id { get; set; } 

        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; }

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }

        /// <summary>
        /// is_update_stock
        /// </summary>
        public bool is_update_stock { get; set; }

        /// <summary>
        /// job_type
        /// </summary>
        public byte job_type { get; set; }

        /// <summary>
        /// source_table_id
        /// </summary>
        public int source_table_id { get; set; }


        #endregion
    }
}
