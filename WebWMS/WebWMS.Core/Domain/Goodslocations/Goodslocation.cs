using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Goodslocations
{
    public class Goodslocation:BaseModel
    {
        #region Property

        /// <summary>
        /// warehouse_id
        /// </summary>
        public int warehouse_id { get; set; } 
        /// <summary>
        /// warehouse_name
        /// </summary>
        public string warehouse_name { get; set; }

        /// <summary>
        /// warehouse_area_name
        /// </summary>
        public string warehouse_area_name { get; set; }

        /// <summary>
        /// warehouse_area_property
        /// </summary>
        public byte warehouse_area_property { get; set; }

        /// <summary>
        /// location_name
        /// </summary>
        public string location_name { get; set; }

        /// <summary>
        /// location_length
        /// </summary>
        public decimal location_length { get; set; }

        /// <summary>
        /// location_width
        /// </summary>
        public decimal location_width { get; set; }

        /// <summary>
        /// location_heigth
        /// </summary>
        public decimal location_heigth { get; set; }

        /// <summary>
        /// location_volume
        /// </summary>
        public decimal location_volume { get; set; }

        /// <summary>
        /// location_load
        /// </summary>
        public decimal location_load { get; set; }

        /// <summary>
        /// roadway_number
        /// </summary>
        public string roadway_number { get; set; }

        /// <summary>
        /// shelf_number
        /// </summary>
        public string shelf_number { get; set; }

        /// <summary>
        /// layer_number
        /// </summary>
        public string layer_number { get; set; }

        /// <summary>
        /// tag_number
        /// </summary>
        public string tag_number { get; set; }

 
        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; }

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }

        /// <summary>
        /// warehouse_area_id
        /// </summary>
        public int warehouse_area_id { get; set; }


        #endregion
    }
}
