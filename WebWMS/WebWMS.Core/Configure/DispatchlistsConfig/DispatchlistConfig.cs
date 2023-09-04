using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Asns;
using WebWMS.Core.Domain.Dispatchlists;

namespace WebWMS.Core.Configure.DispatchlistsConfig
{
    public class DispatchlistConfig : IEntityTypeConfiguration<Dispatchlist>
    {
        public void Configure(EntityTypeBuilder<Dispatchlist> builder)
        {
            builder.ToTable("Dispatchlists");
            builder.HasMany(d=>d.detailList).WithOne(dl=>dl.dispatchlist);
        }
    }
}
