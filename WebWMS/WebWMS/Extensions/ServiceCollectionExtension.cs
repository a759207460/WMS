using Azure;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WebWMS.Core.DbContexts;
using WebWMS.Core.Domain.Asns;
using WebWMS.Core.Repositorys;

namespace WebWMS.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void InitialDb(this IServiceCollection services,IConfigurationRoot configurationRoot)
        {
            services.AddDbContext<WMSDbContext>(option => option.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")));
            services.AddUnitOfWork<WMSDbContext>();
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterService(this IServiceCollection services)
        {

        }
    }
}
