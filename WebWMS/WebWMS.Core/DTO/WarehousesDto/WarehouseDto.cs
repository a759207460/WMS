using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.Warehouses
{
    public class WarehouseDto : BaseDto
    {
        #region Property

        /// <summary>
        /// warehouse_name
        /// </summary>
        public string warehouse_name { get; set; } = string.Empty;

        /// <summary>
        /// city
        /// </summary>
        public string city { get; set; } = string.Empty;

        /// <summary>
        /// address
        /// </summary>
        public string address { get; set; } = string.Empty;

        /// <summary>
        /// email
        /// </summary>
        public string email { get; set; } = string.Empty;

        /// <summary>
        /// manager
        /// </summary>
        public string manager { get; set; } = string.Empty;

        /// <summary>
        /// contact_tel
        /// </summary>
        public string contact_tel { get; set; } = string.Empty;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;


        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } = false;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 0;


        #endregion
    }
}
