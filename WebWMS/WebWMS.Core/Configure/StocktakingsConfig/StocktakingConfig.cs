using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Stocktakings;

namespace WebWMS.Core.Configure.StocktakingsConfig
{
    public class StocktakingConfig : IEntityTypeConfiguration<Stocktaking>
    {
        public void Configure(EntityTypeBuilder<Stocktaking> builder)
        {
            builder.ToTable("Stocktakings");
        }
    }
}
