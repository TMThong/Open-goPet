using GopetHost.Data;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace GopetHost
{
    public class Program
    {
        public static void Main(string[] args)
        {

            string input = "3rsadfg 234 sgf nap admin sdafasdfasdf"; // Ví dụ đầu vào
            string pattern = @"\bnap\b\s+(?<username>[a-zA-Z0-9_]+)\b"; // Biểu thức chính quy

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                Console.WriteLine("Tìm thấy 'nap' và tên người dùng: " + match.Groups["username"].Value);
            }
            else
            {
                Console.WriteLine("Không tìm thấy 'nap' hoặc tên người dùng.");
            }


            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
#if DEBUG
            var AppConnectionStr = builder.Configuration.GetConnectionString("AppConnectionStrLocal");
#elif !DEBUG
            var AppConnectionStr = builder.Configuration.GetConnectionString("AppConnectionStrLocal");
#endif
            builder.Services.AddDbContext<AppDatabaseContext>(options => options.UseMySql(AppConnectionStr, ServerVersion.AutoDetect(AppConnectionStr)));
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
