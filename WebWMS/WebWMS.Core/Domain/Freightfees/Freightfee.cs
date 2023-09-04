using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Freightfees
{
    public class Freightfee:BaseModel
    {
        #region Property

        /// <summary>
        /// carrier
        /// </summary>
        public string carrier { get; set; }

        /// <summary>
        /// departure_city
        /// </summary>
        public string departure_city { get; set; }

        /// <summary>
        /// arrival_city
        /// </summary>
        public string arrival_city { get; set; }

        /// <summary>
        /// price_per_weight
        /// </summary>
        public decimal price_per_weight { get; set; }

        /// <summary>
        /// price_per_volume
        /// </summary>
        public decimal price_per_volume { get; set; }

        /// <summary>
        /// min_payment
        /// </summary>
        public decimal min_payment { get; set; }

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
