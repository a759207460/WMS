using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Asns
{
    public class Asnsort:BaseModel
    {
        #region Property

        /// <summary>
        /// asn_id
        /// </summary>
        public int asn_id { get; set; }

        /// <summary>
        /// sorted_qty
        /// </summary>
        public int sorted_qty { get; set; }
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
