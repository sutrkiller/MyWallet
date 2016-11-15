using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyWallet.Configuration;
using MyWallet.Entities.Configuration;
using MyWallet.Entities.Repositories;
using MyWallet.Entities.Repositories.Interfaces;
using MyWallet.Services.Configuration;
using MyWallet.Services.Services;
using MyWallet.Services.Services.Interfaces;

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
            services.Configure<ConnectionOptions>(
                    options => options.ConnectionString = Configuration.GetConnectionString("MyWalletConnection"))
                .AddScoped<IBudgetService, BudgetService>().
                AddScoped<IEntryService, EntryService>();
            // Add framework services.
            services.AddMvc();

            services.AddTransient<IBudgetRepository, BudgetRepository>();
            services.AddTransient<IEntryRepository, EntryRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            var mapper = new MapperConfiguration(cfg =>
            {
                ViewModelsMapperConfiguration.InitializeMappings(cfg);
                ServicesMapperConfiguration.InitializeMappings(cfg);
            }).CreateMapper();

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
