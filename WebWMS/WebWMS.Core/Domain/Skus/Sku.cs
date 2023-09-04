using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Skus
{
    public class Sku:BaseModel
    {
        #region Property


        public Spu Spu { get; set; }

        public int SpuId { get; set; }

        /// <summary>
        /// sku_code
        /// </summary>
        public string sku_code { get; set; }

        /// <summary>
        /// sku_name
        /// </summary>
        public string sku_name { get; set; }

        /// <summary>
        /// weight
        /// </summary>
        public decimal weight { get; set; }

        /// <summary>
        /// lenght
        /// </summary>
        public decimal lenght { get; set; }

        /// <summary>
        /// width
        /// </summary>
        public decimal width { get; set; }

        /// <summary>
        /// height
        /// </summary>
        public decimal height { get; set; }

        /// <summary>
        /// volume
        /// </summary>
        public decimal volume { get; set; }

        /// <summary>
        /// unit
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// cost
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// price
        /// </summary>
        public decimal price { get; set; }

 

        #endregion
    }
}
