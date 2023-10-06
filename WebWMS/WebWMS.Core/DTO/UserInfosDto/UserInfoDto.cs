using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Users;

namespace WebWMS.Core.DTO.UserInfosDto
{
    public class UserInfoDto:BaseDto
    {
        #region Property

        public List<UserAndRolesDto> Roles { get; set; }

        public List<int> RoleIds { get; set; }

        public string RoleNames { get; set; }

        /// <summary>
        /// customer name
        /// </summary>
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
        /// 是否删除
        /// </summary>
        public bool IsRemove { get; set; }

        /// <summary>
        /// contact tel
        /// </summary>
        public string MoblePhone { get; set; }

        /// <summary>
        /// creator
        /// </summary>
        public string? Creator { get; set; }

        #endregion
    }
}
