using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Skus;

namespace WebWMS.Core.Configure.SkusConfig
{
    public class SpuConfig : IEntityTypeConfiguration<Spu>
    {
        public void Configure(EntityTypeBuilder<Spu> builder)
        {
            builder.ToTable("Spus");
        }
    }
}
