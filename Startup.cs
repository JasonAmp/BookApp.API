using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Storage.EFCore;
using BookAPP.Api.Utils;
using BookAPP.API.Services;
using BookAPP.API.Utils;
using BookAPP.Core;
using BookAPP.Core.Services;
using BookAPP.Store;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            services.AddDbContext<ApplicationDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("default")); });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Version", new OpenApiInfo { Title = "BookApp Apis", Version = "v1" });
            });


            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                   new OpenApiInfo
                   {
                       Title = "BookApp Apis",
                       Version = "v1",
                       Description = "BookApp Apis",
                   }
                );
            });

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<Message>();

            services.AddHandlers();

            services.AddDomainServices();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseSwagger(c => c.RouteTemplate = "BookApp/swagger/{documentName}/swagger.json");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/BookApp/swagger/v1/swagger.json", "BookApp Apis V1");
                c.RoutePrefix = "BookApp";

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
