using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebWMS.Core.DTO.UserInfosDto
{
    public class ExportUserInfDto
    {
        public string? Name { get; set; }

        /// <summary>
        /// customer name
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// contact tel
        /// </summary>
        public string MoblePhone { get; set; }

        /// <summary>
        /// email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// address
        /// </summary>
        public string? Address { get; set; }


        /// <summary>
        /// manager
        /// </summary>
        public string IsEnabled { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsRemove { get; set; }

    }
}
