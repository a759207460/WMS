using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.Rolemenus
{
    public class RolemenuDto: BaseDto
    {
        #region Property

        /// <summary>
        /// userrole_id
        /// </summary>
        public int userrole_id { get; set; } = 0;

        /// <summary>
        /// menu_id
        /// </summary>
        public int menu_id { get; set; } = 0;

        /// <summary>
        /// authority
        /// </summary>
        public byte authority { get; set; } = 1;

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 1;


        #endregion
    }
}
