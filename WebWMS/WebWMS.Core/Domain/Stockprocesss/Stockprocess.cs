using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Stockprocesss
{
    public class Stockprocess:BaseModel
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
        /// process_status
        /// </summary>
        public bool process_status { get; set; }

        /// <summary>
        /// processor
        /// </summary>
        public string processor { get; set; }

        /// <summary>
        /// process_time
        /// </summary>
        public DateTime process_time { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; }

 
        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion


        #region detail table

        /// <summary>
        /// detail table
        /// </summary>
        public List<Stockprocessdetail> detailList { get; set; }

        #endregion
    }
}
