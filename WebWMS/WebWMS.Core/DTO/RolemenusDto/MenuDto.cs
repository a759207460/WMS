using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.Rolemenus
{
    public class MenuDto: BaseDto
    {
        #region Property

        /// <summary>
        /// menu_name
        /// </summary>
        public string menu_name { get; set; } = string.Empty;

        /// <summary>
        /// module
        /// </summary>
        public string module { get; set; } = string.Empty;

        /// <summary>
        /// vue_path
        /// </summary>
        public string vue_path { get; set; } = string.Empty;

        /// <summary>
        /// vue_path_detail
        /// </summary>
        public string vue_path_detail { get; set; } = string.Empty;

        /// <summary>
        /// vue_directory
        /// </summary>
        public string vue_directory { get; set; } = string.Empty;

        /// <summary>
        /// sort
        /// </summary>
        public int sort { get; set; } = 0;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 1;


        #endregion
    }
}
