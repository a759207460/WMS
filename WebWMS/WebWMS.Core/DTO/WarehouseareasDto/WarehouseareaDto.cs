using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.Warehouseareas
{
    public class WarehouseareaDto : BaseDto
    {
        #region Property

        /// <summary>
        /// warehouse_id
        /// </summary>
        public int warehouse_id { get; set; } = 0;

        /// <summary>
        /// area_name
        /// </summary>
        public string area_name { get; set; } = string.Empty;

        /// <summary>
        /// parent_id
        /// </summary>
        public int parent_id { get; set; } = 0;

        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } = false;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 0;

        /// <summary>
        /// area_property
        /// </summary>
        public byte area_property { get; set; } = 0;


        #endregion
    }
}
