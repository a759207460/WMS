using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Stockmoves;

namespace WebWMS.Core.Configure.StockmovesConfig
{
    public class StockmoveConfig : IEntityTypeConfiguration<Stockmove>
    {
        public void Configure(EntityTypeBuilder<Stockmove> builder)
        {
            builder.ToTable("Stockmoves");
        }
    }
}
