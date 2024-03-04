﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NerdStore.WebApp.MVC.Setup;
using NerdStore.WebApp.MVC.Extensions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.HttpOverrides;
using NerdStore.Core.Extensions;
using NerdStore.Vendas.Data;
using NerdStore.WebApp.MVC.Areas.Data.Extensions;
using NerdStore.Vendas.Data.Constants;

namespace NerdStore.WebApp.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcConfiguration();                               

            services.AddDbContext<VendasContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(ContextConstants.DB_CONNECTION_NAME)));  

            services.AddIdentityConfiguration(Configuration);

            services.AddMediatR(typeof(Startup));

            services.ResolveDependencies();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });


            services.Configure<AppSettings>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();
            app.UseExceptionHandler("/erro/500");
            app.UseStatusCodePagesWithRedirects("/erro/{0}");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.SeedIdentityData(Configuration);
            
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");                

                endpoints.MapRazorPages();

            });            

        }
    }
}
