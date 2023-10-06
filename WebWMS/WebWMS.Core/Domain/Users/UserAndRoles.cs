using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Roles;
using WebWMS.Core.DTO.RolesDto;

namespace WebWMS.Core.Domain.Users
{
    public class UserAndRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserInfo User { get; set; }
        public int RoleId { get; set; }
        public RoleInfo Role { get; set; }
    }
}
