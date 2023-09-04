using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Asns;

namespace WebWMS.Core.Configure.AsnsConfig
{
    public class AsnsortConfig : IEntityTypeConfiguration<Asnsort>
    {
        public void Configure(EntityTypeBuilder<Asnsort> builder)
        {
            builder.ToTable("Asnsort");
        }
    }
}
