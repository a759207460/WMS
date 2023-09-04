using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Asns
{
    public class Asn : BaseModel
    {

        #region Property

        /// <summary>
        /// asn_no
        /// </summary>
        public string asn_no { get; set; }

        /// <summary>
        /// asn_status
        /// </summary>
        public byte asn_status { get; set; }

        /// <summary>
        /// spu_id
        /// </summary>
        public int spu_id { get; set; }

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// asn_qty
        /// </summary>
        public int asn_qty { get; set; }

        /// <summary>
        /// actual_qty
        /// </summary>
        public int actual_qty { get; set; }

        /// <summary>
        /// sorted_qty
        /// </summary>
        public int sorted_qty { get; set; }

        /// <summary>
        /// shortage_qty
        /// </summary>
        public int shortage_qty { get; set; }

        /// <summary>
        /// more_qty
        /// </summary>
        public int more_qty { get; set; }

        /// <summary>
        /// damage_qty
        /// </summary>
        public int damage_qty { get; set; }

        /// <summary>
        /// weight
        /// </summary>
        public decimal weight { get; set; }

        /// <summary>
        /// volume
        /// </summary>
        public decimal volume { get; set; }

        /// <summary>
        /// supplier_id
        /// </summary>
        public int supplier_id { get; set; }

        /// <summary>
        /// supplier_name
        /// </summary>
        public string supplier_name { get; set; }

        /// <summary>
        /// goods_owner_id
        /// </summary>
        public int goods_owner_id { get; set; }

        /// <summary>
        /// goods_owner_name
        /// </summary>
        public string goods_owner_name { get; set; }

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
