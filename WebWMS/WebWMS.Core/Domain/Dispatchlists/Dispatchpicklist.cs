using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Dispatchlists
{
    public class Dispatchpicklist:BaseModel
    {
        #region Property


        /// <summary>
        /// goods_owner_id
        /// </summary>
        public int goods_owner_id { get; set; }

        /// <summary>
        /// goods_location_id
        /// </summary>
        public int goods_location_id { get; set; }

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// pick_qty
        /// </summary>
        public int pick_qty { get; set; }

        /// <summary>
        /// picked_qty
        /// </summary> = 0;

        /// <summary>
        /// is_update_stock
        /// </summary>
        public bool is_update_stock { get; set; }

        public Dispatchlist dispatchlist { get; set; }  
        #endregion
    }
}
