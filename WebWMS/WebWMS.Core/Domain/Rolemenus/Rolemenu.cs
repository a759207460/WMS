using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Rolemenus
{
    public class Rolemenu:BaseModel
    {
        #region Property

        /// <summary>
        /// userrole_id
        /// </summary>
        public int userrole_id { get; set; }

        /// <summary>
        /// menu_id
        /// </summary>
        public int menu_id { get; set; }

        /// <summary>
        /// authority
        /// </summary>
        public byte authority { get; set; }

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion
    }
}
