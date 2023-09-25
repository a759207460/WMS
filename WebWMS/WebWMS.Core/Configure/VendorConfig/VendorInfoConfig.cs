using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Vendors;

namespace WebWMS.Core.Configure.VendorConfig
{
    public class VendorInfoConfig : IEntityTypeConfiguration<VendorInfo>
    {
        public void Configure(EntityTypeBuilder<VendorInfo> builder)
        {
            builder.ToTable("Vendors");
        }
    }
}
