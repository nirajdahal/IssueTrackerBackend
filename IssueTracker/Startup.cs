using AutoMapper;
using IssueTracker.Extensions;
using Library.Entities;
using Library.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using NLog;
using System;
using System.IO;

namespace IssueTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.ConfigureCors();
  
            services.ConfigureLoggerService();
            services.ConfigureIISIntegration();
   
            services.ConfigureSqlContextProduction(Configuration);

            services.ConfigureIdentityService();
            services.ConfigureJwtService(Configuration);
            services.ConfigureIdenittyPassword();
            services.AddAutoMapper(typeof(Startup));


            services.AddControllersWithViews(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                x.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }
            );
            services.ConfigureRepositiryManager();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "";
                    await next();
                }
            });


            app.UseHttpsRedirection();


           
            app.UseDefaultFiles(); 
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors(
                options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            ); //This needs to set everything allowed

            app.UseAuthorization();

         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               
            });

        }
        }
    }
