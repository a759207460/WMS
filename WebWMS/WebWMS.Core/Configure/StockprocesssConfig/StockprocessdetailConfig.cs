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
    public class StockprocessdetailConfig : IEntityTypeConfiguration<Stockprocessdetail>
    {
        public void Configure(EntityTypeBuilder<Stockprocessdetail> builder)
        {
            builder.ToTable("Stockprocessdetails");
        }
    }
}
