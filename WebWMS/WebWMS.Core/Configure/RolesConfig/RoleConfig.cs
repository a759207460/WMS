using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Roles;

namespace WebWMS.Core.Configure.RolesConfig
{
    public class RoleConfig : IEntityTypeConfiguration<RoleInfo>
    {
        public void Configure(EntityTypeBuilder<RoleInfo> builder)
        {
            builder.ToTable("Roles");
            builder.HasMany(r=>r.Users).WithMany(r=>r.Roles);
            builder.HasMany(r => r.Menus).WithMany(r => r.Roles);
        }
    }
}
