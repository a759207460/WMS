using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Dispatchlists;

namespace WebWMS.Core.Configure.DispatchlistsConfig
{
    public class DispatchpicklistConfig : IEntityTypeConfiguration<Dispatchpicklist>
    {
        public void Configure(EntityTypeBuilder<Dispatchpicklist> builder)
        {
            builder.ToTable("Dispatchpicklists");
        }
    }
}
