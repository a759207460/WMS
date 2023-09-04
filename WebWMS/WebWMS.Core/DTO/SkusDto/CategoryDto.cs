using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.Skus
{
    public class CategoryDto: BaseDto
    {
        #region Property

        /// <summary>
        /// category_name
        /// </summary>
        public string category_name { get; set; } = string.Empty;

        /// <summary>
        /// parent_id
        /// </summary>
        public int parent_id { get; set; } = 0;

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
