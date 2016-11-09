using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWallet.Entities.Configuration;
using MyWallet.Services.Services;
using MyWallet.Services.Services.Interfaces;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Entities.Repositories;
using AutoMapper;
using MyWallet.Configuration;
using MyWallet.Services.Configuration;

namespace MyWallet
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConnectionOptions>(options => options.ConnectionString = Configuration.GetConnectionString("MyWalletConnection"))
            .AddScoped<IBudgetService, BudgetService>();
            // Add framework services.
            services.AddMvc();

            services.AddTransient<IBudgetRepository, BudgetRepository>();


            var mapper = new MapperConfiguration(cfg =>
            {
                ViewModelsMapperConfiguration.InitialializeMappings(cfg);
                ServicesMapperConfiguration.InitialializeMappings(cfg);
            }).CreateMapper();

            // Do not allow application to start with broken configuration. Fail fast.
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
