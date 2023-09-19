using CommonLibraries.Redis;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Runtime;
using WebWMS.AutoMapper;
using WebWMS.Core.DbContexts;
using WebWMS.Extensions;
using NLog;
using NLog.Web;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace WebWMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();

            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Login.html";
                option.LogoutPath = "/";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(120);
            });
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();
            builder.Services.InitialDb(configurationRoot);//注册数据库上下文对象服务
            builder.Services.RegisterService();//注册各类服务
            builder.Services.AddAutoMapper(c => c.AddProfile(new AutoMapperProFile()));
            builder.Services.AddOptions().Configure<RedisSetting>(r => configurationRoot.GetSection("RedisConnectionString").Bind(r));
            builder.Host.UseNLog();
            builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            var app = builder.Build();

            //设置登录页面
            DefaultFilesOptions filesOptions = new DefaultFilesOptions();
            filesOptions.DefaultFileNames.Clear();
            filesOptions.DefaultFileNames.Add("Login.html");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseDefaultFiles(filesOptions);
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}