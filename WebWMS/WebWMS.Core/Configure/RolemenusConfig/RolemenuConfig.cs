using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Rolemenus;

namespace WebWMS.Core.Configure.RolemenusConfig
{
    public class RolemenuConfig : IEntityTypeConfiguration<Rolemenu>
    {
        public void Configure(EntityTypeBuilder<Rolemenu> builder)
        {
            builder.ToTable("Rolemenus");
        }
    }
}
