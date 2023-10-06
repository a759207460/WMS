using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.Domain.Companys;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.Domain.Menus;
using System.Numerics;
using WebWMS.Core.Domain.Vendors;
using WebWMS.Core.Domain.Roles;

namespace WebWMS.Core.DbContexts
{
    public class WMSDbContext : DbContext
    {
        public WMSDbContext(DbContextOptions<WMSDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companys { get; set; }
        public DbSet<UserInfo> Customers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<VendorInfo> Vendors { get; set; }
        public DbSet<RoleInfo> Roles { get; set; }
        public DbSet<MenuRole> MenuAndRoles { get; set; }
        public DbSet<UserAndRoles> UserAndRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
