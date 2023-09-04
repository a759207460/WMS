using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Warehouseareas;

namespace WebWMS.Core.Configure.WarehouseareasConfig
{
    public class WarehouseareaConfig : IEntityTypeConfiguration<Warehousearea>
    {
        public void Configure(EntityTypeBuilder<Warehousearea> builder)
        {
            builder.ToTable("Warehouseareas");
        }
    }
}
