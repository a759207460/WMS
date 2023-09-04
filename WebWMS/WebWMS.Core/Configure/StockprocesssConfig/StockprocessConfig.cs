using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Stockprocesss;

namespace WebWMS.Core.Configure.StockprocesssConfig
{
    public class StockprocessConfig : IEntityTypeConfiguration<Stockprocess>
    {
        public void Configure(EntityTypeBuilder<Stockprocess> builder)
        {
            builder.ToTable("Stockprocesss");
            builder.HasMany(s => s.detailList).WithOne(s => s.Stockprocess);
        }
    }
}
