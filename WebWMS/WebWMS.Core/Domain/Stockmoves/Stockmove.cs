using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stockmoves
{
    public class Stockmove:BaseModel
    {
        #region Property

        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }

        /// <summary>
        /// move_status
        /// </summary>
        public byte move_status { get; set; }

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// orig_goods_location_id
        /// </summary>
        public int orig_goods_location_id { get; set; }

        /// <summary>
        /// dest_googs_location_id
        /// </summary>
        public int dest_googs_location_id { get; set; }

        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// goods_owner_id
        /// </summary>
        public int goods_owner_id { get; set; }

        /// <summary>
        /// handler
        /// </summary>
        public string handler { get; set; }

        /// <summary>
        /// handle_time
        /// </summary>
        public DateTime handle_time { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } 

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion
    }
}
