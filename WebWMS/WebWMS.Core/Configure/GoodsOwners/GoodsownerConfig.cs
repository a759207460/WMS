using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.GoodsOwners;

namespace WebWMS.Core.Configure.GoodsOwners
{
    public class GoodsownerConfig : IEntityTypeConfiguration<Goodsowner>
    {
        public void Configure(EntityTypeBuilder<Goodsowner> builder)
        {
            builder.ToTable("Goodsowners");
        }
    }
}
