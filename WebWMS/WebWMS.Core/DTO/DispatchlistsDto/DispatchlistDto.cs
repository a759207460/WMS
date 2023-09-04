using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;

namespace WebWMS.Core.DTO.Dispatchlists
{
    public class DispatchlistDto:BaseDto
    {
        #region Property

        /// <summary>
        /// dispatch_no
        /// </summary>
        public string dispatch_no { get; set; } = string.Empty;

        /// <summary>
        /// dispatch_status
        /// </summary>
        public byte dispatch_status { get; set; } = 0;

        /// <summary>
        /// customer_id
        /// </summary>
        public int customer_id { get; set; } = 0;

        /// <summary>
        /// customer_name
        /// </summary>
        public string customer_name { get; set; } = string.Empty;

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; } = 0;

        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; } = 0;

        /// <summary>
        /// weight
        /// </summary>
        public decimal weight { get; set; } = 0;

        /// <summary>
        /// volume
        /// </summary>
        public decimal volume { get; set; } = 0;

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; } = string.Empty;


        /// <summary>
        /// damage_qty
        /// </summary>
        public int damage_qty { get; set; } = 0;

        /// <summary>
        /// lock_qty
        /// </summary>
        public int lock_qty { get; set; } = 0;

        /// <summary>
        /// picked_qty
        /// </summary>
        public int picked_qty { get; set; } = 0;

        /// <summary>
        /// intrasit_qty
        /// </summary>
        public int intrasit_qty { get; set; } = 0;

        /// <summary>
        /// package_qty
        /// </summary>
        public int package_qty { get; set; } = 0;

        /// <summary>
        /// weighing_qty
        /// </summary>
        public int weighing_qty { get; set; } = 0;

        /// <summary>
        /// actual_qty
        /// </summary>
        public int actual_qty { get; set; } = 0;

        /// <summary>
        /// sign_qty
        /// </summary>
        public int sign_qty { get; set; } = 0;

        /// <summary>
        /// package_no
        /// </summary>
        public string package_no { get; set; } = string.Empty;

        /// <summary>
        /// package_person
        /// </summary>
        public string package_person { get; set; } = string.Empty;

        /// <summary>
        /// package_time
        /// </summary>
        public DateTime package_time { get; set; }

        /// <summary>
        /// weighing_no
        /// </summary>
        public string weighing_no { get; set; } = string.Empty;

        /// <summary>
        /// weighing_person
        /// </summary>
        public string weighing_person { get; set; } = string.Empty;

        /// <summary>
        /// weighing_weight
        /// </summary>
        public decimal weighing_weight { get; set; } = 0;

        /// <summary>
        /// waybill_no
        /// </summary>
        public string waybill_no { get; set; } = string.Empty;

        /// <summary>
        /// carrier
        /// </summary>
        public string carrier { get; set; } = string.Empty;

        /// <summary>
        /// freightfee
        /// </summary>
        public decimal freightfee { get; set; } = 0;

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; } = 0;


        #endregion


        #region detail table

        /// <summary>
        /// detail table
        /// </summary>
        public List<Dispatchpicklist> detailList { get; set; } = new List<Dispatchpicklist>(2);

        #endregion
    }
}
