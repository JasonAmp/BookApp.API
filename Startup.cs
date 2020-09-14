using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPP.API.Services;
using BookAPP.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;


namespace BookAPP.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Version", new OpenApiInfo { Title = "BookApp-API Apis", Version = "v1" });
            });

            
            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                   new OpenApiInfo
                   {
                       Title = "BookApp-API Apis",
                       Version = "v1",
                       Description = "BookApp-API Apis",
                   }
                );
            services.AddScoped<IBookService, BookService>();

        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            
            app.UseSwagger(c => c.RouteTemplate = "BookApp-API/swagger/{documentName}/swagger.json");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/BookApp-API/swagger/v1/swagger.json", "BookApp-API Apis V1");
                c.RoutePrefix = "BookApp-API";

            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
