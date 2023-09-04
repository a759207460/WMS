using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.suppliers;

namespace WebWMS.Core.Configure.suppliersConfig
{
    public class supplierConfig: IEntityTypeConfiguration<supplier>
    {
        public void Configure(EntityTypeBuilder<supplier> builder)
        {
            builder.ToTable("suppliers");
        }
    }
}
