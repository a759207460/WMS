﻿using Azure;
using CommonLibraries.API;
using CommonLibraries.Excel;
using CommonLibraries.Redis;
using Microsoft.EntityFrameworkCore;
using WebWMS.Common;
using WebWMS.Core.DbContexts;
using WebWMS.Core.Domain.Companys;
using WebWMS.Core.Domain.Customers;
using WebWMS.Core.Domain.Menus;
using WebWMS.Core.Domain.Products;
using WebWMS.Core.Domain.Roles;
using WebWMS.Core.Domain.Users;
using WebWMS.Core.Domain.Vendors;
using WebWMS.Core.Repositorys;
using WebWMS.Core.Services.CompanysService;
using WebWMS.Core.Services.CustomersService;
using WebWMS.Core.Services.MenusService;
using WebWMS.Core.Services.ProductsService;
using WebWMS.Core.Services.RolesService;
using WebWMS.Core.Services.UserInfosService;
using WebWMS.Core.Services.VendorsService;

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
            services.AddScoped<IUserInfoService,UserInfoService>();
            services.AddScoped<IRepository<UserInfo>, Repository<UserInfo>>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRepository<Menu>, Repository<Menu>>();
            services.AddScoped<IRepository<Company>, Repository<Company>>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IRepository<VendorInfo>, Repository<VendorInfo>>();
            services.AddScoped<IVendorInfoService, VendorInfoService>();
            services.AddScoped<IRepository<Customer>, Repository<Customer>>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IRepository<RoleInfo>, Repository<RoleInfo>>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuRoleInfoService, MenuRoleInfoService>();
            services.AddScoped<IRepository<Product>, Repository<Product>>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<HelpGetMenuList>();
            services.AddScoped<RedisClientHelper>();
        }

        /// <summary>
        /// 注册配置文件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configurationRoot"></param>
        public static void RegisterConfigure(this IServiceCollection services, IConfigurationRoot configurationRoot)
        {
            services.AddOptions().Configure<RedisSetting>(r => configurationRoot.GetSection("RedisConnectionString").Bind(r));
            services.AddOptions().Configure<ExceConfig>(r => configurationRoot.GetSection("ImportAndExport").Bind(r));
            services.AddOptions().Configure<APIConfig>(r => configurationRoot.GetSection("APIUrl").Bind(r));
        }

    }
}
