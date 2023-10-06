using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.DTO.MenusDto;

namespace WebWMS.Core.DTO.RolesDto
{
    public class RoleDto : BaseDto
    {
        public string RoleName { get; set; }

        public string RoleCode { get; set; }

        public List<MenuRole> Menus { get; set; }

        public List<UserInfo> Users { get; set; }
    }
}
