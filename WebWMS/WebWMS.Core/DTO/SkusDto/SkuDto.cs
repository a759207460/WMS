using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.Skus
{
    public class SkuDto: BaseDto
    {
        #region Property

        /// <summary>
        /// spu_id
        /// </summary>
        public int spu_id { get; set; } = 0;

        /// <summary>
        /// sku_code
        /// </summary>
        public string sku_code { get; set; } = string.Empty;

        /// <summary>
        /// sku_name
        /// </summary>
        public string sku_name { get; set; } = string.Empty;

        /// <summary>
        /// weight
        /// </summary>
        public decimal weight { get; set; } = 0;

        /// <summary>
        /// lenght
        /// </summary>
        public decimal lenght { get; set; } = 0;

        /// <summary>
        /// width
        /// </summary>
        public decimal width { get; set; } = 0;

        /// <summary>
        /// height
        /// </summary>
        public decimal height { get; set; } = 0;

        /// <summary>
        /// volume
        /// </summary>
        public decimal volume { get; set; } = 0;

        /// <summary>
        /// unit
        /// </summary>
        public string unit { get; set; } = string.Empty;

        /// <summary>
        /// cost
        /// </summary>
        public decimal cost { get; set; } = 0;

        /// <summary>
        /// price
        /// </summary>
        public decimal price { get; set; } = 0;

 

        #endregion
    }
}
