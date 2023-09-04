using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Stockadjusts;

namespace WebWMS.Core.Configure.StockadjustsConfig
{
    public class StockadjustConfig : IEntityTypeConfiguration<Stockadjust>
    {
        public void Configure(EntityTypeBuilder<Stockadjust> builder)
        {
            builder.ToTable("Stockadjusts");
        }
    }
}
