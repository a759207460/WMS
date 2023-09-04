using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Warehouseareas
{
    public class Warehousearea : BaseModel
    {
        #region Property

        /// <summary>
        /// warehouse_id
        /// </summary>
        public int warehouse_id { get; set; }

        /// <summary>
        /// area_name
        /// </summary>
        public string area_name { get; set; }

        /// <summary>
        /// parent_id
        /// </summary>
        public int parent_id { get; set; }

        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; }

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }

        /// <summary>
        /// area_property
        /// </summary>
        public byte area_property { get; set; }


        #endregion
    }
}
