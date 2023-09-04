using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stocktakings
{
    public class Stocktaking:BaseModel
    {
        #region Property

        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }

        /// <summary>
        /// job_status
        /// </summary>
        public bool job_status { get; set; }

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
        /// book_qty
        /// </summary>
        public int book_qty { get; set; }

        /// <summary>
        /// counted_qty
        /// </summary>
        public int counted_qty { get; set; }

        /// <summary>
        /// difference_qty
        /// </summary>
        public int difference_qty { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; }

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }

        /// <summary>
        /// handler
        /// </summary>
        public string handler { get; set; } 

        /// <summary>
        /// handle_time
        /// </summary>
        public DateTime handle_time { get; set; }

        #endregion
    }
}
