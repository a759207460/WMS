using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.AsnsDto
{ 
    public class AsnsortDto:BaseDto
    {
        #region Property

        /// <summary>
        /// asn_id
        /// </summary>
        public int asn_id { get; set; } = 0;

        /// <summary>
        /// sorted_qty
        /// </summary>
        public int sorted_qty { get; set; } = 0;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;

        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } = true;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 1;

        #endregion
    }
}
