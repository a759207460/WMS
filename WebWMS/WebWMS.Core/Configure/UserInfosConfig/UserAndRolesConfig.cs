using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Users;

namespace WebWMS.Core.Configure.UserInfosConfig
{
    public class UserAndRolesConfig : IEntityTypeConfiguration<UserAndRoles>
    {
        public void Configure(EntityTypeBuilder<UserAndRoles> builder)
        {
            builder.ToTable("UserAndRoles");
            builder.HasKey(x => x.Id);
            builder.Property(p => p.UserId);
            builder.Property(p => p.RoleId);
        }
    }
}
