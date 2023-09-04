using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.Freightfees
{
    public class FreightfeeDto:BaseDto
    {
        #region Property

        /// <summary>
        /// carrier
        /// </summary>
        public string carrier { get; set; } = string.Empty;

        /// <summary>
        /// departure_city
        /// </summary>
        public string departure_city { get; set; } = string.Empty;

        /// <summary>
        /// arrival_city
        /// </summary>
        public string arrival_city { get; set; } = string.Empty;

        /// <summary>
        /// price_per_weight
        /// </summary>
        public decimal price_per_weight { get; set; } = 0;

        /// <summary>
        /// price_per_volume
        /// </summary>
        public decimal price_per_volume { get; set; } = 0;

        /// <summary>
        /// min_payment
        /// </summary>
        public decimal min_payment { get; set; } = 0;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;

 
        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } = false;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 0;


        #endregion
    }
}
