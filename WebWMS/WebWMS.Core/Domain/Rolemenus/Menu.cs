using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Rolemenus
{
    public class Menu:BaseModel
    {
        #region Property

        /// <summary>
        /// menu_name
        /// </summary>
        public string menu_name { get; set; }

        /// <summary>
        /// module
        /// </summary>
        public string module { get; set; }

        /// <summary>
        /// vue_path
        /// </summary>
        public string vue_path { get; set; }

        /// <summary>
        /// vue_path_detail
        /// </summary>
        public string vue_path_detail { get; set; }

        /// <summary>
        /// vue_directory
        /// </summary>
        public string vue_directory { get; set; }

        /// <summary>
        /// sort
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion
    }
}
