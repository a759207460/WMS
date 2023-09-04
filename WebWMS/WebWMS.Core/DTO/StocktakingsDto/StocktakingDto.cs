using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.Stocktakings
{
    public class StocktakingDto: BaseDto
    {
        #region Property

        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; } = string.Empty;

        /// <summary>
        /// job_status
        /// </summary>
        public bool job_status { get; set; } = false;

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; } = 0;

        /// <summary>
        /// goods_owner_id
        /// </summary>
        public int goods_owner_id { get; set; } = 0;

        /// <summary>
        /// goods_location_id
        /// </summary>
        public int goods_location_id { get; set; } = 0;

        /// <summary>
        /// book_qty
        /// </summary>
        public int book_qty { get; set; } = 0;

        /// <summary>
        /// counted_qty
        /// </summary>
        public int counted_qty { get; set; } = 0;

        /// <summary>
        /// difference_qty
        /// </summary>
        public int difference_qty { get; set; } = 0;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 1;

        /// <summary>
        /// handler
        /// </summary>
        public string handler { get; set; } = string.Empty;

        /// <summary>
        /// handle_time
        /// </summary>
        public DateTime handle_time { get; set; }

        #endregion
    }
}
