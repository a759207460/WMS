using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Skus
{
    public class Spu : BaseModel
    {
        #region Property

        /// <summary>
        /// spu_code
        /// </summary>
        public string spu_code { get; set; }

        /// <summary>
        /// spu_name
        /// </summary>
        public string spu_name { get; set; }

        /// <summary>
        /// category_id
        /// </summary>
        public int category_id { get; set; } 

        /// <summary>
        /// spu_description
        /// </summary>
        public string spu_description { get; set; }

        /// <summary>
        /// bar_code
        /// </summary>
        public string bar_code { get; set; }

        /// <summary>
        /// supplier_id
        /// </summary>
        public int supplier_id { get; set; }

        /// <summary>
        /// supplier_name
        /// </summary>
        public string supplier_name { get; set; }

        /// <summary>
        /// brand
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// origin
        /// </summary>
        public string origin { get; set; }

        /// <summary>
        /// length_unit
        /// </summary>
        public byte length_unit { get; set; }
        /// <summary>
        /// volume_unit
        /// </summary>
        public byte volume_unit { get; set; }

        /// <summary>
        /// weight_unit
        /// </summary>
        public byte weight_unit { get; set; }

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

        #region details
        /// <summary>
        /// sku
        /// </summary>
        public List<Sku> detailList { get; set; }
        #endregion
    }
}
