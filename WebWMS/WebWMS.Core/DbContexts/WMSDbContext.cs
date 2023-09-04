using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Asns;
using WebWMS.Core.Domain.Companys;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.Domain.Dispatchlists;
using WebWMS.Core.Domain.Freightfees;
using WebWMS.Core.Domain.Goodslocations;
using WebWMS.Core.Domain.GoodsOwners;
using WebWMS.Core.Domain.Rolemenus;
using WebWMS.Core.Domain.Skus;
using WebWMS.Core.Domain.Stockadjusts;
using WebWMS.Core.Domain.Stockfreezes;
using WebWMS.Core.Domain.Stockmoves;
using WebWMS.Core.Domain.Stockprocesss;
using WebWMS.Core.Domain.Stocks;
using WebWMS.Core.Domain.Stocktakings;
using WebWMS.Core.Domain.suppliers;
using WebWMS.Core.Domain.Warehouseareas;
using WebWMS.Core.Domain.Warehouses;

namespace WebWMS.Core.DbContexts
{
    public class WMSDbContext : DbContext
    {
        public WMSDbContext(DbContextOptions<WMSDbContext> options) : base(options)
        {

        }
        public DbSet<Asn> Asns { get; set; }
        public DbSet<Asnsort> Asnsorts { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dispatchlist> Dispatchlists { get; set; }
        public DbSet<Dispatchpicklist> Dispatchpicklists { get; set; }
        public DbSet<Freightfee> Freightfees { get; set; }
        public DbSet<Goodslocation> Goodslocations { get; set; }
        public DbSet<Goodsowner> Goodsowners { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Rolemenu> Rolemenus { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Stockadjust> Stockadjusts { get; set; }
        public DbSet<Stockfreeze> Stockfreezes { get; set; }
        public DbSet<Stockmove> Stockmoves { get; set; }
        public DbSet<Stockprocess> Stockprocesss { get; set; }
        public DbSet<Stockprocessdetail> Stockprocessdetails { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Stocktaking> Stocktakings { get; set; }
        public DbSet<supplier> suppliers { get; set; }
        public DbSet<Warehousearea> Warehouseareas { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
