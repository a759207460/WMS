﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Skus;

namespace WebWMS.Core.Configure.SkusConfig
{
    public class SkuConfig : IEntityTypeConfiguration<Sku>
    {
        public void Configure(EntityTypeBuilder<Sku> builder)
        {
            builder.ToTable("Skus");
            builder.Property(s=>s.SpuId).IsRequired();
            builder.HasOne(s => s.Spu).WithMany(s=>s.detailList);
        }
    }
}