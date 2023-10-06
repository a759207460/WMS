using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Roles;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.DTO.RolesDto;

namespace WebWMS.Core.DTO.UserInfosDto
{
    public class UserAndRolesDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserInfoDto User { get; set; }
        public int RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
