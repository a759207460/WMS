using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Roles;

namespace WebWMS.Core.Domain.Menus
{
    public class MenuRole
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public int RoleId { get; set; }
        public RoleInfo Role { get; set; }
    }
}
