﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Goodslocations;

namespace WebWMS.Core.Configure.GoodslocationsConfig
{
    public class GoodslocationConfig : IEntityTypeConfiguration<Goodslocation>
    {
        public void Configure(EntityTypeBuilder<Goodslocation> builder)
        {
            builder.ToTable("Goodslocations");
        }
    }
}
