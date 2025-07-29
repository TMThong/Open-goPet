using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
[assembly: ApiController]
namespace Gopet.APIs
{
    
    public class HttpServer
    {
        private WebApplication Application { get;  }


        public int Port { get; }

        public HttpServer(int port) : this()
        {
            Port = port;
        }

        protected HttpServer()
        {
            var builder = WebApplication.CreateBuilder(Array.Empty<string>());

            builder.Services.AddControllers();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gopet API", Description = "Documention of GopetServer", Version = "v1" });
            });



            Application = builder.Build();


            Application.UseHttpsRedirection();

            Application.UseAuthorization();

            Application.MapControllers();

            Application.UseRouting();

            Application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Application.UseSwagger();

            Application.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GopetServer API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        public void Start()
        {
            Application.RunAsync($"http://0.0.0.0:{this.Port}");
        }

        public void Stop()
        {
            Application.StopAsync();
        }
    }
}
