using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebWMS.Core.DbContexts;
using WebWMS.Core.Domain.Customers;

namespace WebWMS.Core.Repositorys.CustomerRepositors
{
    public class CustomerRepositor:Repository<Customer>
    {
        public CustomerRepositor(WMSDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
