using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stockprocesss
{
    public class Stockprocessdetail : BaseModel
    {
        #region Property

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
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }

        /// <summary>
        /// is_source
        /// </summary>
        public bool is_source { get; set; }

        /// <summary>
        /// is_update_stock
        /// </summary>
        public bool is_update_stock { get; set; }

        public Stockprocess Stockprocess { get; set; }
        #endregion
    }
}
