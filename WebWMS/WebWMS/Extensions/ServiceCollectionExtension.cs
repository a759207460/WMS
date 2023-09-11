using Azure;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebWMS.Core.DbContexts;
using WebWMS.Core.Domain.Asns;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.Domain.Rolemenus;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Repositorys.CustomerRepositors;
using WebWMS.Core.Services.CustomersService;
using WebWMS.Core.Services.RolemenusService;

namespace WebWMS.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void InitialDb(this IServiceCollection services,IConfigurationRoot configurationRoot)
        {
            services.AddDbContext<WMSDbContext>(option => option.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")));
            //services.AddUnitOfWork<WMSDbContext>();
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService,CustomerService>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRepository<Menu>, Repository<Menu>>();
        }

    }
}
