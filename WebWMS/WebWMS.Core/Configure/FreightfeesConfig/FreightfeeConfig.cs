using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Freightfees;

namespace WebWMS.Core.Configure.FreightfeesConfig
{
    public class FreightfeeConfig : IEntityTypeConfiguration<Freightfee>
    {
        public void Configure(EntityTypeBuilder<Freightfee> builder)
        {
            builder.ToTable("Freightfees");
        }
    }
}
