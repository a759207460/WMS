using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.Domain.Roles;

namespace WebWMS.Core.Configure.MenusConfig
{
    public class MenuRoleConfig : IEntityTypeConfiguration<MenuRole>
    {
        public void Configure(EntityTypeBuilder<MenuRole> builder)
        {
            builder.ToTable("MenuAndRoles");
            builder.HasKey(m =>m.Id);
        }
    }
}
