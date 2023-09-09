using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DTO.Customers;

namespace WebWMS.Core.Domain.Customers
{
    public  class Customer:BaseModel
    {
        #region Property

        public string? Name { get; set; }

        /// <summary>
        /// customer name
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// city
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// address
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// manager
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// contact tel
        /// </summary>
        public string MoblePhone { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string? Creator { get; set; }

        public bool IsRemove { get; set; }

        #endregion
    }
}
