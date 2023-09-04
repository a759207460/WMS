using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stockfreezes
{
    public class Stockfreeze : BaseModel
    {
        #region Property

        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; }

        /// <summary>
        /// job_type
        /// </summary>
        public bool job_type { get; set; } 

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
        /// handler
        /// </summary>
        public string handler { get; set; }

   
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion
    }
}
