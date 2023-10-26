﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Products;

namespace WebWMS.Core.Configure.ProductsConfig
{
    public class ProductAttributeConfig : IEntityTypeConfiguration<ProductAttributes>
    {
        public void Configure(EntityTypeBuilder<ProductAttributes> builder)
        {
            builder.ToTable("ProducesAttributes");
        }
    }
}
