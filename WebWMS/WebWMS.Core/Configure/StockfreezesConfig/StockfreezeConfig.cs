using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Stockfreezes;

namespace WebWMS.Core.Configure.StockfreezesConfig
{
    public class StockfreezeConfig : IEntityTypeConfiguration<Stockfreeze>
    {
        public void Configure(EntityTypeBuilder<Stockfreeze> builder)
        {
            builder.ToTable("Stockfreezes");
        }
    }
}
