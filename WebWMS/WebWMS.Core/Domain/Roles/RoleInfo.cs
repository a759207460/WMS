using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.Domain.Users;

namespace WebWMS.Core.Domain.Roles
{
    public class RoleInfo:BaseModel
    {
        public string RoleName { get; set; }

        public string RoleCode { get; set; }

        public ICollection<MenuRole> Menus { get; set; }
    }
}
