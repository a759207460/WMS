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
    public class AsnConfig : IEntityTypeConfiguration<Asn>
    {
        public void Configure(EntityTypeBuilder<Asn> builder)
        {
            builder.ToTable("Asns");
        }
    }
}
