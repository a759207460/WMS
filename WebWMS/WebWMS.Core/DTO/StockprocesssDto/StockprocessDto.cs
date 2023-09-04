using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.Stockprocesss
{
    public class StockprocessDto: BaseDto
    {

        #region Property

        /// <summary>
        /// job_code
        /// </summary>
        public string job_code { get; set; } = string.Empty;

        /// <summary>
        /// job_type
        /// </summary>
        public bool job_type { get; set; } = false;

        /// <summary>
        /// process_status
        /// </summary>
        public bool process_status { get; set; } = false;

        /// <summary>
        /// processor
        /// </summary>
        public string processor { get; set; } = string.Empty;

        /// <summary>
        /// process_time
        /// </summary>
        public DateTime process_time { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 0;


        #endregion


        #region detail table

        /// <summary>
        /// detail table
        /// </summary>
        public List<StockprocessdetailDto> detailList { get; set; } = new List<StockprocessdetailDto>(2);

        #endregion
    }
}
