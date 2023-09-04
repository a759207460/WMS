using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stocks
{
    public class Stock:BaseModel
    {
        #region Property

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// goods_location_id
        /// </summary>
        public int goods_location_id { get; set; } 

        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// goods_owner_id
        /// </summary>
        public int goods_owner_id { get; set; }

        /// <summary>
        /// is_freeze
        /// </summary>
        public bool is_freeze { get; set; }

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion
    }
}
