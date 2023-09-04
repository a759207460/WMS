using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO;

namespace WebWMS.Core.DTO.Skus
{
    public class SpuDto : BaseDto
    {
        #region Property

        /// <summary>
        /// spu_code
        /// </summary>
        public string spu_code { get; set; } = string.Empty;

        /// <summary>
        /// spu_name
        /// </summary>
        public string spu_name { get; set; } = string.Empty;

        /// <summary>
        /// category_id
        /// </summary>
        public int category_id { get; set; } = 0;

        /// <summary>
        /// spu_description
        /// </summary>
        public string spu_description { get; set; } = string.Empty;

        /// <summary>
        /// bar_code
        /// </summary>
        public string bar_code { get; set; } = string.Empty;

        /// <summary>
        /// supplier_id
        /// </summary>
        public int supplier_id { get; set; } = 0;

        /// <summary>
        /// supplier_name
        /// </summary>
        public string supplier_name { get; set; } = string.Empty;

        /// <summary>
        /// brand
        /// </summary>
        public string brand { get; set; } = string.Empty;

        /// <summary>
        /// origin
        /// </summary>
        public string origin { get; set; } = string.Empty;

        /// <summary>
        /// length_unit
        /// </summary>
        public byte length_unit { get; set; } = 0;

        /// <summary>
        /// volume_unit
        /// </summary>
        public byte volume_unit { get; set; } = 0;

        /// <summary>
        /// weight_unit
        /// </summary>
        public byte weight_unit { get; set; } = 0;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;

 
        /// <summary>
        /// is_valid
        /// </summary>
        public bool is_valid { get; set; } = true;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 1;


        #endregion

        #region details
        /// <summary>
        /// sku
        /// </summary>
        public List<SkuDto> detailList { get; set; } = new List<SkuDto>();
        #endregion
    }
}
