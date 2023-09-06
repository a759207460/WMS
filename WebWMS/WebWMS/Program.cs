using Microsoft.EntityFrameworkCore;
using WebWMS.AutoMapper;
using WebWMS.Core.DbContexts;
using WebWMS.Extensions;

namespace WebWMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).AddEnvironmentVariables().Build();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.InitialDb(configurationRoot);//ע�����ݿ������Ķ������
            builder.Services.RegisterService();//ע��������
            builder.Services.AddAutoMapper(c => c.AddProfile(new AutoMapperProFile()));
            var app = builder.Build();

            //���õ�¼ҳ��
            DefaultFilesOptions filesOptions=new DefaultFilesOptions();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}