using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Skus
{
    public class Category:BaseModel
    {
        #region Property

        /// <summary>
        /// category_name
        /// </summary>
        public string category_name { get; set; }

        /// <summary>
        /// parent_id
        /// </summary>
        public int parent_id { get; set; }

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
