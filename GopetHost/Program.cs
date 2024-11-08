using GopetHost.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Net;
using System.Text.RegularExpressions;

namespace GopetHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
#if DEBUG
            var AppConnectionStr = builder.Configuration.GetConnectionString("AppConnectionStrLocal");
#elif !DEBUG
            var AppConnectionStr = builder.Configuration.GetConnectionString("AppConnectionStr");
            Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            builder.Host.UseSerilog();
#endif

            builder.Services.AddDbContext<AppDatabaseContext>(options => options.UseMySql(AppConnectionStr, ServerVersion.AutoDetect(AppConnectionStr), mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure();
            }));
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.Use(async (ctx, next) =>
            {
                await next();

                if (ctx.Response.StatusCode == 404)
                {
                    string originalPath = ctx.Request.Path.Value;
                    ctx.Items["originalPath"] = originalPath;
                    ctx.Request.Path = "/Home/PageNotFound";
                    ctx.Response.StatusCode = 200;
                    await next();
                }
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>()
                {
                    {
                        ".apk",
                        "application/vnd.android.package-archive"
                    }
                })
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
