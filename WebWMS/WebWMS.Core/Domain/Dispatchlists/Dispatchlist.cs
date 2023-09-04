using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.Domain.Dispatchlists
{
    public class Dispatchlist:BaseModel
    {
        #region Property

        /// <summary>
        /// dispatch_no
        /// </summary>
        public string dispatch_no { get; set; }

        /// <summary>
        /// dispatch_status
        /// </summary>
        public byte dispatch_status { get; set; }

        /// <summary>
        /// customer_id
        /// </summary>
        public int customer_id { get; set; }

        /// <summary>
        /// customer_name
        /// </summary>
        public string customer_name { get; set; }

        /// <summary>
        /// sku_id
        /// </summary>
        public int sku_id { get; set; }

        /// <summary>
        /// qty
        /// </summary>
        public int qty { get; set; }

        /// <summary>
        /// weight
        /// </summary>
        public decimal weight { get; set; }

        /// <summary>
        /// volume
        /// </summary>
        public decimal volume { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string creator { get; set; }


        /// <summary>
        /// damage_qty
        /// </summary>
        public int damage_qty { get; set; }

        /// <summary>
        /// lock_qty
        /// </summary>
        public int lock_qty { get; set; }

        /// <summary>
        /// picked_qty
        /// </summary>
        public int picked_qty { get; set; }

        /// <summary>
        /// intrasit_qty
        /// </summary>
        public int intrasit_qty { get; set; }

        /// <summary>
        /// package_qty
        /// </summary>
        public int package_qty { get; set; }

        /// <summary>
        /// weighing_qty
        /// </summary>
        public int weighing_qty { get; set; }

        /// <summary>
        /// actual_qty
        /// </summary>
        public int actual_qty { get; set; }

        /// <summary>
        /// sign_qty
        /// </summary>
        public int sign_qty { get; set; } 

        /// <summary>
        /// package_no
        /// </summary>
        public string package_no { get; set; }

        /// <summary>
        /// package_person
        /// </summary>
        public string package_person { get; set; }

        /// <summary>
        /// package_time
        /// </summary>
        public DateTime package_time { get; set; }

        /// <summary>
        /// weighing_no
        /// </summary>
        public string weighing_no { get; set; }

        /// <summary>
        /// weighing_person
        /// </summary>
        public string weighing_person { get; set; }

        /// <summary>
        /// weighing_weight
        /// </summary>
        public decimal weighing_weight { get; set; }

        /// <summary>
        /// waybill_no
        /// </summary>
        public string waybill_no { get; set; }

        /// <summary>
        /// carrier
        /// </summary>
        public string carrier { get; set; }

        /// <summary>
        /// freightfee
        /// </summary>
        public decimal freightfee { get; set; }

        /// <summary>
        /// tenant_id
        /// </summary>
        public long tenant_id { get; set; }


        #endregion


        #region detail table

       
        /// <summary>
        /// detail table
        /// </summary>
        public List<Dispatchpicklist> detailList { get; set; }

        #endregion
    }
}
